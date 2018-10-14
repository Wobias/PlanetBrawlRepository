using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController_SaturnShield : MonoBehaviour, IDamageable
{
    public int hitBlocks;

    private float ionDamage = 0;
    private PlayerController controller;


    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        controller.SetPlayerProtection(true);
    }

    public void Hit(float physicalDmg, float effectDps, DamageType dmgType, Vector2 knockbackForce, float stunTime, float effectTime = 0)
    {
        hitBlocks--;

        if (hitBlocks < 1)
        {
            Kill();
        }
    }

    public void IonDamage(float dps)
    {
        ionDamage = dps;

        Kill();
    }

    public void StopIon()
    {
        return;
    }

    void Kill()
    {
        controller.SetPlayerProtection(false, ionDamage);
        Destroy(gameObject);
    }
}
