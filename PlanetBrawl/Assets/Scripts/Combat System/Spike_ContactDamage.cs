using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike_ContactDamage : MonoBehaviour
{
    [SerializeField]
    public float physicalDmg;
    [SerializeField]
    public float effectDps;
    [SerializeField]
    public DamageType dmgType = DamageType.physical;
    [SerializeField]
    public float knockback;
    [SerializeField]
    public float stunTime;
    [SerializeField]
    public float effectTime;

    protected IDamageable target;

    public Transform checkStart;
    public float checkLength;
    public LayerMask hitMask;

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
                target.Hit(physicalDmg, effectDps, dmgType, (hit.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
            }

            Destroy(transform.parent.gameObject);
        }
    }

    protected void GetTarget(Collider2D other)
    {
        target = null;
        target = other.GetComponent<IDamageable>();
        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(checkStart.position, checkStart.right * checkLength);
        Gizmos.DrawRay(checkStart.position, checkStart.up * checkLength);
        Gizmos.DrawRay(checkStart.position, -checkStart.up * checkLength);
    }
}
