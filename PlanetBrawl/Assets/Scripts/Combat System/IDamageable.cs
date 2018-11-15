using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageType { none, physical, poison, fire, ice};

public interface IDamageable
{
    void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, float effectTime=0);
    void IonDamage(float dps);
    void StopIon();
}