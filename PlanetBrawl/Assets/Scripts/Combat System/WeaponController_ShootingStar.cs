﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_ShootingStar : WeaponController
{
    //VARIABLES
    #region

    public float shotSpeed = 1;
    public float lifetime = 1;

    private bool fired = false;

    #endregion


    public override void Aim(Vector2 direction)
    {
        if (fired)
            return;

        base.Aim(direction);
    }

    public override bool Shoot(bool isFirePressed)
    {
        if (isFirePressed && !fired && canAttack)
        {
            for (int i = 0; i < weaponParts.Length; i++)
            {
                ShootFragment(weaponParts[i], weaponColliders[i]);
            }
            Destroy(gameObject, lifetime);
            fired = true;
        }

        return true;
    }

    private void ShootFragment(Rigidbody2D frag, Collider2D fragCollider)
    {
        frag.isKinematic = false; //Unlock the moons position
        fragCollider.enabled = true;
        frag.velocity = -(origin.position - frag.transform.position).normalized * shotSpeed;
        frag.transform.SetParent(null);
    }
}
