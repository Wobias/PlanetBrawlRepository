using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState {full, average, low, critical};

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //Health Variables
    #region
    public float health = 100f;
    public float hpPercent = 100f;
    private float maxHealth;
    #endregion
    
    public static HealthState healthState = HealthState.full;

    Transform myTransform;

    Vector3 scaleVector;
    Vector3 minScale = new Vector3(0.6f, 0.6f, 0.6f);

    void Awake()
    {
        //Set max health
        maxHealth = health;
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        health -= 10f;
    //        StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
    //        DownScaling(health);

    //        if (health <= 0f)
    //        {
    //            Die();
    //        }
    //    }
    //}

    void OnEnable()
    {
        myTransform = GetComponent<Transform>();
    }

    //IDamageable method
    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;
            StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
            DownScaling(health);
        }
        else
        {
            Die();
        }
    }

    //Die method
    public void Die()
    {
        Debug.Log("player dead");
        Destroy(gameObject);
    }


    void DownScaling(float hp)
    {
        //Get current health in percent
        hpPercent = (hp * 100f) / maxHealth;

        //Set Scale, Speed, Sprite in relation to current health
        if (hpPercent <= 75f && hpPercent > 50f)
        {
            scaleVector = new Vector3(0.9f, 0.9f, 0.9f);
            myTransform.localScale = scaleVector;
            healthState = HealthState.average;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
        if (hpPercent <= 50f && hpPercent > 25f)
        {
            scaleVector = new Vector3(0.75f, 0.75f, 0.75f);
            myTransform.localScale = scaleVector;
            healthState = HealthState.low;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
        if (hpPercent <= 25f)
        {
            scaleVector = minScale;
            myTransform.localScale = scaleVector;
            healthState = HealthState.critical;
            GetComponent<ISpeedable>().SpeedToSize(healthState);
            GetComponent<PlanetSpriteSwitch>().SwitchPlanetSprite(healthState);
        }
    }
}
