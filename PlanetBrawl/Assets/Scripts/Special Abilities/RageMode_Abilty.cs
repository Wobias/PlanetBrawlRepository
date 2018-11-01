﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageMode_Abilty : MonoBehaviour, ISpecialAbility
{
    public GameObject flameThrower;
    public float knockbackForce;
    public float selfDamage;
    public float dmgRate = 0.5f;

    private bool rageMode = false;
    private Rigidbody2D rb2d;
    private IDamageable selfTarget;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        selfTarget = GetComponent<IDamageable>();
    }

    public void Use()
    {
        if (!rageMode)
        {
            flameThrower.SetActive(true);
            rageMode = true;
            StartCoroutine(DamageSelf());
        }

        rb2d.AddForce(-(flameThrower.transform.position - transform.position).normalized * knockbackForce);
    }

    public void StopUse()
    {
        flameThrower.SetActive(false);
        rageMode = false;
        StopAllCoroutines();
    }

    IEnumerator DamageSelf()
    {
        yield return new WaitForSeconds(dmgRate);
        selfTarget.Hit(selfDamage, 0, DamageType.physical, Vector2.zero, 0);

        if (rageMode)
        {
            StartCoroutine(DamageSelf());
        }
    }
}
