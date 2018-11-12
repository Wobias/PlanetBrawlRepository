using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_ContactDamage : Weapon_ContactDamage
{
    protected override void OnCollisionEnter2D(Collision2D col)
    {
        base.OnCollisionEnter2D(col);
        Destroy(gameObject);
    }
}
