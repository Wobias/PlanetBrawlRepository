﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskHealth_Pickup : MonoBehaviour
{
    public float healthBonus;
    public float damage;
    public float dmgStunTime = 0.25f;

    private Player_HealthController target;
    bool heal;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform.root.GetComponent<Player_HealthController>();

        //Hit the target if it is damageable
        if (target != null)
        {
            heal = Random.Range(0, 10) <= 5;

            if (heal)
            {
                target.Heal(healthBonus);
            }
            else
            {
                target.Hit(damage, DamageType.physical, Vector2.zero, dmgStunTime);
            }
            
            Destroy(gameObject);
        }
    }
}