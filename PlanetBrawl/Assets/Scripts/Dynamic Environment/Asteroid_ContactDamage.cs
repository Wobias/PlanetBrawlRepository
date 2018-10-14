using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid_ContactDamage : Weapon_ContactDamage
{
    public float selfDamage = 1;
    public float selfKnockback = 500;
    public float selfStunTime = 0.2f;

    private IDamageable healthController;


    protected override void Start()
    {
        healthController = GetComponent<IDamageable>();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        return;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GetTarget(collision.collider);

        //Hit the target if it is damageable
        target?.Hit(physicalDmg, effectDps, dmgType, (collision.collider.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
        healthController.Hit(selfDamage, 0, DamageType.physical, (collision.collider.transform.position - transform.position).normalized * -selfKnockback, selfStunTime);
    }
}