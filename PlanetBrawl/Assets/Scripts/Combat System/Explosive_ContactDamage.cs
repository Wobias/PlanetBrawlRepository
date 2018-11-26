using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive_ContactDamage : Weapon_ContactDamage
{
    public float radius;
    public LayerMask hitMask;


    protected override void Start()
    {
        hitMask = hitMask & ~(1 << gameObject.layer);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        Explode();
    }

    protected override void OnCollisionEnter2D(Collision2D col)
    {
        Explode();
    }

    public void Explode()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hitMask);

        for (int i = 0; i < hits.Length; i++)
        {
            GetTarget(hits[i]);

            //Hit the target if it is damageable
            if (target != null)
            {
                target.Hit(physicalDmg, dmgType, (hits[i].transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);
                AudioManager1.instance.Play(hitsound);
            }
        }

        Destroy(gameObject);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
