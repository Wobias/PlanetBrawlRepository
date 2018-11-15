using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    //Health Variables
    #region
    public float health = 100f;
    public DamageType imunity = DamageType.none;
    private static float iceSlowMultiplier = 0.5f;

    public GameObject fireParticles;
    public GameObject iceParticles;
    public GameObject poisonParticles;

    public bool invincible = false;

    [HideInInspector]
    public bool stunned = false;
    
    private bool frozen = false;
    private bool burning = false;
    private bool poisoned = false;

    protected float ionDamage;

    protected Coroutine poisonRoutine;
    protected Coroutine fireRoutine;
    protected Coroutine iceRoutine;
    protected Coroutine stunRoutine;

    protected float maxHealth;

    protected bool dpsApplied = false;
    protected bool dpsAnim = true;

    protected IMovable movable;
    protected float dpsAnimTimeout = 0.75f;
    protected ISpeedable movement;
    #endregion


    protected virtual void Start()
    {
        //Set max health
        maxHealth = health;
        movable = GetComponent<IMovable>();
        movement = GetComponent<ISpeedable>();
    }

    protected virtual void FixedUpdate()
    {
        if (dpsApplied)
        {
            if (!invincible)
                health -= ionDamage * Time.fixedDeltaTime;

            if (health > 0)
            {
                if (dpsAnim)
                {
                    OnHealthChange();
                    dpsAnim = false;
                    StartCoroutine(AllowDpsAnim());
                }
            }
            else
            {
                Kill();
            }
        }
    }

    //IDamageable method
    public void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, float effectTime = 0)
    {
        if (stunned)
            return;

        if (dmgType != DamageType.none)
        {
            if (stunTime > 0)
                Stun(stunTime);

            if (knockbackForce != Vector2.zero)
                movable.ApplyTempExForce(knockbackForce, stunTime);
        }

        PhysicalHit(physicalDmg);

        switch (dmgType)
        {
            case DamageType.physical:
                break;
            case DamageType.poison:
                Poison(effectTime);
                break;
            case DamageType.fire:
                Burn(effectTime);
                break;
            case DamageType.ice:
                Freeze(effectTime);
                break;
            default:
                break;
        }
    }

    protected void PhysicalHit(float damage)
    {
        if (imunity == DamageType.physical)
            damage *= 0.5f;

        if (!invincible)
            health -= damage;

        if (health > 0)
        {
            OnHealthChange();
        }
        else
        {
            Kill();
        }
    }

    protected void Poison(float effectTime)
    {
        if (imunity == DamageType.poison)
        {
            return;
        }

        if (frozen)
        {
            StopCoroutine(iceRoutine);
            iceRoutine = null;
            SetIce(false);
        }
        else if (burning)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
            SetFire(false);
        }

        if (poisonRoutine != null)
            StopCoroutine(poisonRoutine);

        SetPoison(true);

        poisonRoutine = StartCoroutine(StopPoison(effectTime));
    }

    protected void Burn(float effectTime)
    {
        if (imunity == DamageType.fire)
        {
            return;
        }

        if (frozen)
        {
            StopCoroutine(iceRoutine);
            iceRoutine = null;
            SetIce(false);
        }
        else if (poisoned)
        {
            StopCoroutine(poisonRoutine);
            poisonRoutine = null;
            SetPoison(false);
        }

        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
        }

        SetFire(true);

        fireRoutine = StartCoroutine(StopFire(effectTime));
    }

    protected void Freeze(float effectTime)
    {
        if (imunity == DamageType.ice)
        {
            return;
        }

        if (burning)
        {
            StopCoroutine(fireRoutine);
            fireRoutine = null;
            SetFire(false);
        }
        else if (poisoned)
        {
            StopCoroutine(poisonRoutine);
            poisonRoutine = null;
            SetPoison(false);
        }

        if (iceRoutine != null)
        {
            StopCoroutine(iceRoutine);
        }

        SetIce(true, effectTime);

        iceRoutine = StartCoroutine(StopIce(effectTime));
    }

    public void IonDamage(float dps)
    {
        ionDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
        }
    }

    public virtual void StopIon()
    {
        ionDamage = 0;

        dpsApplied = false;
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }

    public void Stun(float stunTime, bool stunMovement=true)
    {
        if (stunRoutine != null)
            StopCoroutine(stunRoutine);

        stunned = true;

        if (stunMovement)
            StunObject(true);

        stunRoutine = StartCoroutine(StopStun(stunTime));
    }

    protected IEnumerator StopPoison(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetPoison(false);

        poisonRoutine = null;
    }

    protected IEnumerator StopFire(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetFire(false);

        fireRoutine = null;
    }

    protected IEnumerator StopIce(float duration)
    {
        yield return new WaitForSeconds(duration);
        SetIce(false);

        iceRoutine = null;
    }

    protected IEnumerator StopStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        stunned = false;
        StunObject(false);

        stunRoutine = null;
    }

    protected IEnumerator AllowDpsAnim()
    {
        yield return new WaitForSeconds(dpsAnimTimeout);

        dpsAnim = true;
    }

    protected virtual void StunObject(bool stunActive)
    {
        return;
    }

    protected virtual void OnHealthChange(bool damage = true)
    {
        return;
    }

    protected virtual void SetPoison(bool active)
    {
        poisoned = active;
        poisonParticles.SetActive(active);
    }

    protected virtual void SetFire(bool active)
    {
        burning = active;
        fireParticles.SetActive(active);
    }

    protected virtual void SetIce(bool active, float length=0)
    {
        frozen = active;
        iceParticles.SetActive(active);

        if (active)
            movement.SpeedEffect(-iceSlowMultiplier, length);
    }
}
