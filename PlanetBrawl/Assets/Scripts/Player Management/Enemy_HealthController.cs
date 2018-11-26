using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_HealthController : Player_HealthController
{
    protected override void Kill()
    {
        Destroy(gameObject);
    }

    public override void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, int attackNr = 0, float effectTime = 0)
    {
        base.Hit(physicalDmg, dmgType, knockbackForce, stunTime, attackNr, effectTime);
    }

    public override void Heal(float bonusHealth)
    {
        return;
    }

    protected override void SetPoison(bool active)
    {
        base.SetPoison(active);
        planetMovement.invertedMove = active;
        controller.InvertAim(active);
    }

    protected override void SetFire(bool active)
    {
        base.SetFire(active);
        planetMovement.SetBrakes(!active);
    }
}
