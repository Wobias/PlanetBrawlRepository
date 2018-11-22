using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_ContactDamage : Weapon_ContactDamage
{
    public Transform checkStart;
    public float checkLength;
    public LayerMask hitMask;


    protected override void Start()
    {
        base.Start();
        hitMask = hitMask & ~(1 << transform.root.gameObject.layer);
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(checkStart.position, checkStart.right, checkLength, hitMask);

        if (hit)
        {
            RaycastHit2D hitLeft = Physics2D.Raycast(checkStart.position, checkStart.up, checkLength, hitMask);
            RaycastHit2D hitRight = Physics2D.Raycast(checkStart.position, -checkStart.up, checkLength, hitMask);

            if (hitLeft && hitLeft == hit || hitRight && hitRight == hit)
                return;

            GetTarget(hit.collider);

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

                target.Hit(physicalDmg, dmgType, (hit.transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);

                if (gotBuff && buffType == DamageType.physical)
                {
                    physicalDmg /= 2;
                    knockback /= 2;
                    weapon.RemoveElement();
                }

                Destroy(transform.parent.gameObject);
            }
        }
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        return;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkStart.position, checkStart.right * checkLength);
        Gizmos.DrawRay(checkStart.position, checkStart.up * checkLength);
        Gizmos.DrawRay(checkStart.position, -checkStart.up * checkLength);
    }
}
