using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController_ShadowClone : MonoBehaviour, IDamageable
{
    public void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, int attackNr = 0, float effectTime = 0)
    {
        Destroy(gameObject);
    }

    public void IonDamage(float dps)
    {
        Destroy(gameObject);
    }

    public void StopIon()
    {
        return;
    }
}
