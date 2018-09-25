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
    public float hitTimeout;
    private float maxHealth;
    private bool canHit = true;
    private PlayerController controller;
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
        maxHealth = health;
        myTransform = GetComponent<Transform>();
        maxScale = myTransform.localScale;
        SetScaleStates();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    health -= 10f;
        //    StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
        //    DownScaling(health);

        //    if (health <= 0f)
        //    {
        //        Die();
        //    }
        //}
    }

    //IDamageable method
    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;
            StartCoroutine(GetComponentInChildren<FaceSpriteSwitch>().FaceHit());
            DownScaling(health);
            //canHit = false;
            //StartCoroutine(AllowHit());
            StartCoroutine(controller.Stun(hitTimeout));
        }
        else
        {
            //canHit = false;
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

    IEnumerator AllowHit()
    {
        yield return new WaitForSeconds(hitTimeout);
        canHit = true;
    }
}
