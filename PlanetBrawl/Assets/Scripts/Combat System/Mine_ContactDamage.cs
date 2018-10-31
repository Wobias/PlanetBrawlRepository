using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_ContactDamage : Weapon_ContactDamage
{
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Destroy(gameObject);
    }
}
