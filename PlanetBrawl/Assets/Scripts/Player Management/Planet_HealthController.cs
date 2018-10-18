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

    

    public static HealthState healthState = HealthState.full;

    Transform myTransform;

    Vector3 maxScale;

    Vector3 averageHpScale;
    Vector3 lowHpScale;
    Vector3 criticalHpScale;

    #endregion


    protected override void Start()
    {
        base.Start();
        //Set max health
        animator = GetComponent<Animator>();
        controller = GetComponent<IPlanet>();
        myTransform = GetComponentInChildren<SpriteRenderer>().transform;
        maxScale = myTransform.localScale;
        SetScaleStates();
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

    protected override void OnDamage()
    {
        ScalePlanet();
        animator.SetFloat("HealthPercent", hpPercent);
        animator.SetTrigger("Hit");
        controller.SetWeaponDistance();
    }

    protected override void StunObject(bool stunActive)
    {
        controller.Stun(stunActive);
    }

    protected override void Kill()
    {
        GameManager_Prototype.gameManager.RemoveFromLists(gameObject);
        GameManager_Prototype.gameManager.VictoryConditions();
        base.Kill();
    }

    public void Heal(float bonusHealth)
    {
        health += bonusHealth;
        if (health > maxHealth)
            health = maxHealth;

        ScalePlanet();
        controller.SetWeaponDistance();
    }
}
