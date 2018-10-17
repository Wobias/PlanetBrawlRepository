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
        controller = GetComponent<IPlanet>();
        myTransform = transform;
        maxScale = myTransform.localScale;
        SetScaleStates();
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
        else if (hpPercent <= 50f && hpPercent > 25f)
        {
            myTransform.localScale = lowHpScale;
            healthState = HealthState.low;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
        else if(hpPercent <= 25f)
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

    protected override void OnDamage()
    {
        DownScaling(health);
        controller.SetWeaponDistance();
    }

    protected override void StunObject(bool stunActive)
    {
        controller.Stun(stunActive);
    }
}
