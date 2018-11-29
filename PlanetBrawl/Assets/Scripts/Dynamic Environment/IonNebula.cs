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
            if (gotBuff)
            {
                if (buffType == DamageType.physical)
                {
                    physicalDmg *= 2;
                    knockback *= 2;
                }
                else
                {
                    target.Hit(0, buffType, Vector2.zero, 0, playerNr, buffTime);
                    weapon.RemoveElement();
                }
            }

            if (isWeapon)
            {
                target.Hit(physicalDmg, dmgType, (col.transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);
                weapon.OnHit();
                //InstantiateParticle(onHitParticle);
            }
            else
            {
                target.Hit(physicalDmg, dmgType, -transform.position.normalized * knockback, stunTime, playerNr, effectTime);
            }
            AudioManager1.instance.Play(hitsound);

            if (gotBuff && buffType == DamageType.physical)
            {
                physicalDmg /= 2;
                knockback /= 2;
                weapon.RemoveElement();
            }
        }
        else if (isWeapon)
        {
            weapon.OnHit();
        }

        if (destroyOnHit)
        {
            if (isWeapon && weapon.ProjectileCount > 1 || !isWeapon)
            {
                Destroy(gameObject);
            }
            else
            {
                if (weapon != null)
                    Destroy(weapon.gameObject);
            }
        }
    }
}
