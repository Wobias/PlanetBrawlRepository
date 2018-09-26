using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController_SaturnShield : MonoBehaviour, IDamageable
{
    public int shieldUses;

    private PlayerController controller;
    private Rigidbody2D rb2d;
    private bool canHit = true;


    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        rb2d = transform.parent.GetComponent<Rigidbody2D>();

        controller.StartPlayerProtection();
    }

    public void PhysicalHit(float damage, Vector2 knockbackForce, float stunTime)
    {
        if (!canHit)
            return;

        Debug.Log("HIT!!!");

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        shieldUses--;

        if (shieldUses <= 0)
        {
            controller.StopPlayerProtection(0);
            Destroy(gameObject);
        }

        canHit = false;
        StartCoroutine(AllowHit(stunTime));
    }

    public void Poison(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        return;
    }

    public void Burn(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        return;
    }

    public void Freeze(float damage, Vector2 knockbackForce, float stunTime, float thawTime)
    {
        if (!canHit)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        shieldUses--;

        if (shieldUses <= 0)
        {
            controller.StopPlayerProtection(0);
            Destroy(gameObject);
        }

        canHit = false;
        StartCoroutine(AllowHit(stunTime));
    }

    public void IonDamage(float dps)
    {
        controller.StopPlayerProtection(dps);
        Destroy(gameObject);
    }

    public void StopIon()
    {
        return;
    }

    IEnumerator AllowHit(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        canHit = true;
    }
}
