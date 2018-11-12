using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour, IDamageable
{
    //Health Variables
    #region
    public float health = 100f;
    public DamageType imunity = DamageType.none;
    public DamageType weakness = DamageType.none;
    public float burnSpeedBonus = 0.5f;

    public GameObject fireParticles;
    public GameObject iceParticles;
    public GameObject poisonParticles;

    public bool invincible = false;

    [HideInInspector]
    public bool stunned = false;
    [HideInInspector]
    public bool frozen = false;

    protected float poisonDamage;
    protected float fireDamage;
    protected float ionDamage;
    protected float specialDamage;

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
                health -= (poisonDamage + fireDamage + ionDamage) * Time.fixedDeltaTime;

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
    public void Hit(float physicalDmg, float effectDps, DamageType dmgType, Vector2 knockbackForce, float stunTime, float effectTime = 0)
    {
        if (stunned)
            return;

        if (dmgType != DamageType.ion)
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
                Poison(effectDps, effectTime);
                break;
            case DamageType.fire:
                Burn(effectDps, effectTime);
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
        if (weakness == DamageType.physical)
            damage *= 2;

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

    protected void Poison(float dps,  float effectTime)
    {
        if (imunity == DamageType.poison)
        {
            return;
        }
        else if (weakness == DamageType.poison)
        {
            dps *= 2;
        }

        if (poisonRoutine != null)
            StopCoroutine(poisonRoutine);

        poisonDamage = dps;
        poisonParticles.SetActive(true);

        if (!dpsApplied)
        {
            dpsApplied = true;
        }

        poisonRoutine = StartCoroutine(StopPoison(effectTime));
    }

    protected void Burn(float dps, float effectTime)
    {
        if (frozen)
        {
            StopCoroutine(iceRoutine);
            iceRoutine = null;
            InstantThaw();
        }

        if (imunity == DamageType.fire)
        {
            return;
        }  
        else if (weakness == DamageType.fire)
        {
            dps *= 2;
        }

        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
        }
        else if (movement != null && fireDamage == 0)
        {
            movement.SpeedEffect(burnSpeedBonus);
        }   

        fireDamage = dps;
        fireParticles.SetActive(true);

        if (!dpsApplied)
        {
            dpsApplied = true;
        }

        fireRoutine = StartCoroutine(StopFire(effectTime));
    }

    protected void Freeze(float effectTime)
    {
        fireDamage = 0;
        fireParticles.SetActive(false);

        if (imunity == DamageType.ice || frozen)
        {
            return;
        }
        else if (weakness == DamageType.ice)
        {
            effectTime *= 2;
        }

        frozen = true;
        iceParticles.SetActive(true);

        if (!stunned)
            StunObject(true);

        iceRoutine = StartCoroutine(Thaw(effectTime));
    }

    public void IonDamage(float dps)
    {
        if (imunity == DamageType.ion)
            return;

        ionDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
        }
    }

    public virtual void StopIon()
    {
        ionDamage = 0;

        if (poisonDamage == 0 && fireDamage == 0)
        {
            dpsApplied = false;
        }
    }

    protected virtual void Kill()
    {
        Destroy(gameObject);
    }

    protected void Stun(float stunTime)
    {
        if (stunRoutine != null)
            StopCoroutine(stunRoutine);

        stunned = true;
        StunObject(true);
        stunRoutine = StartCoroutine(StopStun(stunTime));
    }

    protected IEnumerator StopPoison(float duration)
    {
        yield return new WaitForSeconds(duration);
        poisonDamage = 0;
        poisonParticles.SetActive(false);

        if (fireDamage == 0 && ionDamage == 0)
        {
            dpsApplied = false;
        }

        poisonRoutine = null;
    }

    protected IEnumerator StopFire(float duration)
    {
        yield return new WaitForSeconds(duration);
        fireDamage = 0;
        fireParticles.SetActive(false);

        if (poisonDamage == 0 && ionDamage == 0)
        {
            dpsApplied = false;
        }

        if (movement != null)
            movement.SpeedEffect(-burnSpeedBonus);

        fireRoutine = null;
    }

    protected IEnumerator Thaw(float duration)
    {
        yield return new WaitForSeconds(duration);
        InstantThaw();

        iceRoutine = null;
    }

    protected IEnumerator StopStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        stunned = false;
        if (!frozen)
            StunObject(false);

        stunRoutine = null;
    }

    protected IEnumerator AllowDpsAnim()
    {
        yield return new WaitForSeconds(dpsAnimTimeout);

        dpsAnim = true;
    }

    protected void InstantThaw()
    {
        frozen = false;
        iceParticles.SetActive(false);
        if (!stunned)
            StunObject(false);
    }

    protected virtual void StunObject(bool stunActive)
    {
        return;
    }

    protected virtual void OnHealthChange(bool damage = true)
    {
        return;
    }
}
