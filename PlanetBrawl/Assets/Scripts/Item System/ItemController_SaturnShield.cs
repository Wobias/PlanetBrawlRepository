using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController_SaturnShield : MonoBehaviour, IDamageable
{
    public int hitBlocks;

    private float ionDamage = 0;
    private PlayerController controller;
    private PlanetMovement movement;
    private Player_HealthController healthController;


    void Start()
    {
        controller = GetComponentInParent<PlayerController>();
        movement = GetComponentInParent<PlanetMovement>();
        healthController = GetComponentInParent<Player_HealthController>();
        controller.SetPlanetProtection(true);
    }

    public void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, int attackNr = 0, float effectTime = 0)
    {
        if (healthController.stunned)
            return;

        hitBlocks--;

        movement.ApplyTempExForce(knockbackForce, stunTime);

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
        controller.SetPlanetProtection(false, ionDamage);
        movement.FlushGravForce();
        Destroy(gameObject);
    }
}
