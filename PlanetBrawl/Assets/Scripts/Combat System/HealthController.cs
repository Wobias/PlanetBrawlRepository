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

    public bool stunned = false;
    public bool frozen = false;

    protected float poisonDamage;
    protected float fireDamage;
    protected float ionDamage;

    protected float maxHealth;

    protected bool dpsApplied = false;

    protected Rigidbody2D rb2d;
    #endregion


    protected virtual void Start()
    {
        //Set max health
        maxHealth = health;
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void FixedUpdate()
    {
        if (dpsApplied)
        {
            health -= (poisonDamage + fireDamage + ionDamage) * Time.fixedDeltaTime;

            if (health > 0)
            {
                OnDamage();
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
            Stun(stunTime);

            if (knockbackForce != Vector2.zero)
                rb2d.AddForce(knockbackForce);
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

        health -= damage;

        if (health > 0)
        {
            OnDamage();
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

        StopCoroutine("StopPoison");

        poisonDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
        }

        StartCoroutine(StopPoison(effectTime));
    }

    protected void Burn(float dps, float effectTime)
    {
        if (frozen)
        {
            StopCoroutine("Thaw");
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

        StopCoroutine("StopFire");

        fireDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
        }

        StartCoroutine(StopFire(effectTime));
    }

    protected void Freeze(float effectTime)
    {
        fireDamage = 0;

        if (imunity == DamageType.ice)
        {
            return;
        }
        else if (weakness == DamageType.ice)
        {
            effectTime *= 2;
        }

        frozen = true;

        if (!stunned)
            StunObject(false);

        StartCoroutine(Thaw(effectTime));
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
        StopCoroutine("StopStun");
        stunned = true;
        StunObject(true);
        StartCoroutine(StopStun(stunTime));
    }

    protected IEnumerator StopPoison(float duration)
    {
        yield return new WaitForSeconds(duration);
        poisonDamage = 0;

        if (fireDamage == 0 && ionDamage == 0)
        {
            dpsApplied = false;
        }
    }

    protected IEnumerator StopFire(float duration)
    {
        yield return new WaitForSeconds(duration);
        fireDamage = 0;

        if (poisonDamage == 0 && ionDamage == 0)
        {
            dpsApplied = false;
        }
    }

    protected IEnumerator Thaw(float duration)
    {
        yield return new WaitForSeconds(duration);
        InstantThaw();
    }

    protected IEnumerator StopStun(float duration)
    {
        yield return new WaitForSeconds(duration);
        stunned = false;
        if (!frozen)
            StunObject(false);
    }

    protected void InstantThaw()
    {
        frozen = false;
        if (!stunned)
            StunObject(false);
    }

    protected virtual void StunObject(bool stunActive)
    {
        return;
    }

    protected virtual void OnDamage()
    {
        return;
    }
}
