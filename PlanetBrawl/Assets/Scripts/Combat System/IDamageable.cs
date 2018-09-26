using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void PhysicalHit(float damage, Vector2 knockbackForce, float stunTime);
    void Poison(float dps, float duration, Vector2 knockbackForce, float stunTime);
    void Burn(float dps, float duration, Vector2 knockbackForce, float stunTime);
    void Freeze(float damage, Vector2 knockbackForce, float stunTime, float thawTime);
    void IonDamage(float dps);
    void StopIon();
}

