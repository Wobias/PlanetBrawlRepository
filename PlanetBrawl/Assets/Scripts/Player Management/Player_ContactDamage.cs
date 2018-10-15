using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ContactDamage : Weapon_ContactDamage
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        return;
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        GetTarget(collision.collider);

        //Hit the target if it is damageable
        target?.Hit(physicalDmg, effectDps, dmgType, (collision.collider.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
    }
}
