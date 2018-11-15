using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_ContactDamage : Weapon_ContactDamage
{
    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (!this.enabled)
            return;

        GetTarget(other);

        //Hit the target if it is damageable
        if (target != null)
        {
            target.Hit(physicalDmg, dmgType, (other.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
            AudioManager1.instance.Play(hitsound);
        }

        Destroy(gameObject);
    }
}
