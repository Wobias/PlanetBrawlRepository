using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState {full, average, low, critical};
public enum DamageType { none, physical, poison, fire, ice};

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //Health Variables
    #region
    public float health = 100f;
    public DamageType imunity = DamageType.none;

    [HideInInspector]
    public bool stunned;
    [HideInInspector]
    public bool frozen = false;

    private float maxHealth;
    private float hpPercent = 100f;
    private PlayerController controller;
    private Rigidbody2D rb2d;
    private float poisonDamage = 0;
    private float fireDamage = 0;
    private float ionDamage = 0;
    private bool dpsApplied = false;
    #endregion
    
    public static HealthState healthState = HealthState.full;

    Transform myTransform;

    Vector3 maxScale;

    Vector3 averageHpScale;
    Vector3 lowHpScale;
    Vector3 criticalHpScale;

    void Awake()
    {
        //Set max health
        controller = GetComponent<PlayerController>();
        rb2d = GetComponent<Rigidbody2D>();
        maxHealth = health;
        myTransform = GetComponent<Transform>();
        maxScale = myTransform.localScale;
        SetScaleStates();
    }

    void OnEnable()
    {
        Debug.Log("Enabled");
    }

    void OnDisable()
    {
        Debug.Log("Disabled");
    }

    void DownScaling(float hp)
    {
        //Get current health in percent
        hpPercent = (hp * 100f) / maxHealth;


        //Set Scale, Speed, Sprite in relation to current health
        if (hpPercent <= 75f && hpPercent > 50f)
        {
            myTransform.localScale = averageHpScale;
            healthState = HealthState.average;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
        if (hpPercent <= 50f && hpPercent > 25f)
        {
            myTransform.localScale = lowHpScale;
            healthState = HealthState.low;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
        if (hpPercent <= 25f)
        {
            myTransform.localScale = criticalHpScale;
            healthState = HealthState.critical;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
    }

    void SetScaleStates()
    {
        averageHpScale = maxScale * 0.9f;
        lowHpScale = maxScale * 0.75f;
        criticalHpScale = maxScale * 0.6f;
    }

    //IDamageable method
    public void PhysicalHit(float damage, Vector2 knockbackForce, float stunTime)
    {
        if (stunned)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        if (imunity != DamageType.physical)
            health -= damage;
        else
            health -= damage * 0.5f;

        if (health > 0)
        {
            StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
            DownScaling(health);
            StartCoroutine(controller.Stun(stunTime));
        }
        else
        {
            Die();
        }
    }

    public void Poison(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        if (stunned)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        StartCoroutine(controller.Stun(stunTime));

        if (imunity != DamageType.poison)
            poisonDamage = dps; 

        if (!dpsApplied)
        {
            dpsApplied = true;
            StartCoroutine(ApplyDamage());
        }

        StartCoroutine(StopPoison(duration));
    }

    public void Burn(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        if (stunned)
            return;

        if (frozen)
            controller.InstantThaw();

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        StartCoroutine(controller.Stun(stunTime));

        if (imunity == DamageType.fire)
            return;

        fireDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
            StartCoroutine(ApplyDamage());
        }

        StartCoroutine(StopFire(duration));
    }

    public void Freeze(float damage, Vector2 knockbackForce, float stunTime, float thawTime)
    {
        if (stunned)
            return;

        fireDamage = 0;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        if (imunity == DamageType.ice)
            return;

        health -= damage;

        if (health > 0)
        {
            StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
            DownScaling(health);
        }
        else
        {
            Die();
        }

        StartCoroutine(controller.Stun(stunTime));
        StartCoroutine(controller.Thaw(thawTime));
    }

    public void IonDamage(float dps)
    {
        ionDamage = dps;

        if (!dpsApplied)
        {
            dpsApplied = true;
            StartCoroutine(ApplyDamage());
        }
    }

    public void StopIon()
    {
        ionDamage = 0;
    }

    IEnumerator ApplyDamage()
    {
        health -= (poisonDamage + fireDamage + ionDamage);

        if (health > 0)
        {
            StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
            DownScaling(health);
        }
        else
        {
            Die();
        }

        yield return new WaitForSeconds(1);

        if (poisonDamage > 0 || fireDamage > 0 || ionDamage > 0)
        {
            StartCoroutine(ApplyDamage());
        }
        else
        {
            dpsApplied = false;
        }
    }

    IEnumerator StopPoison(float duration)
    {
        yield return new WaitForSeconds(duration);
        poisonDamage = 0;
    }

    IEnumerator StopFire(float duration)
    {
        yield return new WaitForSeconds(duration);
        fireDamage = 0;
    }

    //Die method
    public void Die()
    {
        Debug.Log("player dead");
        Destroy(gameObject);
    }
}
