using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlanet
{
    void SetWeaponDistance();
    void Stun(bool stunActive);
    void SetPlanetProtection(bool isActive, float ionPassOnDmg = 0);
}
