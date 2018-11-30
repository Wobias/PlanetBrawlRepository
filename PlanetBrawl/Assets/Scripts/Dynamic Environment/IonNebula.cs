using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonNebula : Weapon_ContactDamage
{
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        GetTarget(col.collider);

        //InstantiateParticle(onHitParticle);

        //Hit the target if it is damageable
        if (target != null)
        {
            target.Hit(physicalDmg, dmgType, -transform.position.normalized * knockback, stunTime, playerNr, effectTime);
            AudioManager1.instance.Play(hitsound);
        }
    }
}
