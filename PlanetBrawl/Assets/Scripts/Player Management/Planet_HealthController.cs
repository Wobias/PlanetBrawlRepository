using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState {full, average, low, critical};

public class Planet_HealthController : HealthController
{
    //Health Variables
    #region
    private float hpPercent = 100f;
    private IPlanet controller;
    private Animator animator;

    private Hat hat;

    public static HealthState healthState = HealthState.full;

    Transform myTransform;
    Vector3 spawnPoint;

    Vector3 maxScale;

    Vector3 averageHpScale;
    Vector3 lowHpScale;
    Vector3 criticalHpScale;

    PlanetMovement planetMovement;

    #endregion


    protected override void Start()
    {
        base.Start();
        //Set max health
        animator = GetComponent<Animator>();
        controller = GetComponent<IPlanet>();
        planetMovement = GetComponent<PlanetMovement>();
        myTransform = GetComponentInChildren<SpriteRenderer>().transform;
        maxScale = myTransform.localScale;
        SetScaleStates();
        spawnPoint = transform.position;
        hat = FindObjectOfType<Hat>();
    }

    void ScalePlanet()
    {
        //Get current health in percent
        hpPercent = (health * 100f) / maxHealth;

        //Set Scale, Speed, Sprite in relation to current health
        if (hpPercent > 75f)
        {
            myTransform.localScale = maxScale;
            healthState = HealthState.full;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
        }
        else if (hpPercent <= 75f && hpPercent > 50f)
        {
            myTransform.localScale = averageHpScale;
            healthState = HealthState.average;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
        }
        else if (hpPercent <= 50f && hpPercent > 25f)
        {
            myTransform.localScale = lowHpScale;
            healthState = HealthState.low;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
        }
        else if(hpPercent <= 25f)
        {
            myTransform.localScale = criticalHpScale;
            healthState = HealthState.critical;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
        }
    }

    void SetScaleStates()
    {
        averageHpScale = maxScale * 0.9f;
        lowHpScale = maxScale * 0.75f;
        criticalHpScale = maxScale * 0.6f;
    }

    protected override void OnHealthChange(bool damage=true)
    {
        //ScalePlanet();
        animator.SetFloat("HealthPercent", hpPercent);
        if (damage)
        {
            animator.SetTrigger("Hit");
        }
        else
        {
            animator.SetTrigger("Heal");
        }
        //controller.SetWeaponDistance();
    }

    protected override void StunObject(bool stunActive)
    {
        controller.Stun(stunActive);
    }

    protected override void Kill()
    {
        if (attackerNr != 0 && GameManager.instance.gameMode == GameModes.deathmatch ||
            GameManager.instance.gameMode == GameModes.teamdeathmatch)
            GameManager.instance.AddScore(attackerNr);

        attackerNr = 0;
        health = maxHealth;
        transform.position = spawnPoint;
        //gameObject.SetActive(false);
    }

    public override void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, int attackNr = 0, float effectTime = 0)
    {
        base.Hit(physicalDmg, dmgType, knockbackForce, stunTime, attackNr, effectTime);
        
        if (hat != null)
        {
            hat.ThrowHat();
        }
    }

    public void Heal(float bonusHealth)
    {
        health += bonusHealth;
        if (health > maxHealth)
            health = maxHealth;

        OnHealthChange(false);
    }

    protected override void SetPoison(bool active)
    {
        base.SetPoison(active);
        planetMovement.invertedMove = active;
        controller.InvertAim(active);
    }

    protected override void SetFire(bool active)
    {
        base.SetFire(active);
        planetMovement.SetBrakes(!active);
    }
}
