using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ContactDamage : Weapon_ContactDamage
{
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D other = collision.collider;

        if (other.gameObject.layer == gameObject.layer)
            return;

        GetTarget(other);

        //Hit the target if it is damageable
        target?.Hit(physicalDmg, dmgType, (collision.collider.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
    }
}
