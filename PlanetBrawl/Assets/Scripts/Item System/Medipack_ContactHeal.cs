using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medipack_ContactHeal : MonoBehaviour
{
    //public bool useWeaponStats = true;

    public float healthBonus;
    public string healSound = "healing";

    protected Planet_HealthController target;


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        GetTarget(other);
        
        //Hit the target if it is damageable
        if (target != null)
        {
            target.Heal(healthBonus);
            AudioManager1.instance.Play(healSound);
            Destroy(gameObject);
        }
    }

    protected void GetTarget(Collider2D other)
    {
        target = null;
        target = other.GetComponent<Planet_HealthController>();
        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<Planet_HealthController>();
        }
    }
}
