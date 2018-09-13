using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    //Health Variables
    #region
    public float health = 100f;
    public float hpPercent = 100f;
    private float maxHealth;
    #endregion

    public enum HealthState {full, average, low, critical};
    public HealthState healthState = HealthState.full;

    Transform myTransform;

    Vector3 scaleVector;
    Vector3 minScale = new Vector3(0.6f, 0.6f, 0.6f);

    //Set max health
    void Awake()
    {
        maxHealth = health;
    }

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
    }

    void DownScaling(float hp)
    {
        //Get current health percentage
        hpPercent = (hp * 100f) / maxHealth;
        Debug.Log(hpPercent);

        //Set Healthstate, Scale, Speed
        if (hpPercent <= 75f && hpPercent > 50f)
        {
            scaleVector = new Vector3(0.9f, 0.9f, 0.9f);
            myTransform.localScale = scaleVector;
            healthState = HealthState.average;
        }
        if (hpPercent <= 50f && hpPercent > 25f)
        {
            scaleVector = new Vector3(0.75f, 0.75f, 0.75f);
            myTransform.localScale = scaleVector;
            healthState = HealthState.low;
        }
        if (hpPercent <= 25f)
        {
            scaleVector = minScale;
            myTransform.localScale = scaleVector;
            healthState = HealthState.critical;
        }
        Debug.Log(myTransform.localScale);
    }
}
