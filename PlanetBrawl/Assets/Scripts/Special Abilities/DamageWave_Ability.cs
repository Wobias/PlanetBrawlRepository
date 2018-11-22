﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageWave_Ability : MonoBehaviour, ISpecialAbility
{
    public string hitsound = "punch";
    public string iceSound = "iceFreeze";

    public float radius;
    public float cooldown;
    public LayerMask hitMask;
    public ParticleSystem specialParticles;
    public ParticleSystem waveParticles;

    [SerializeField]
    public float physicalDmg;
    [SerializeField]
    public DamageType dmgType = DamageType.physical;
    [SerializeField]
    public float knockback;
    [SerializeField]
    public float stunTime;
    [SerializeField]
    public float effectTime;

    private bool canAttack = true;
    private bool pressed = false;

    private int playerNr = 0;

    private IDamageable target;


    void Start()
    {
        playerNr = GetComponent<PlayerController>().playerNr;

        hitMask = hitMask & ~(1 << gameObject.layer);
    }

    void HitCollider(Collider2D other)
    {
        GetTarget(other);

        //Hit the target if it is damageable
        if (target != null)
        {
            target.Hit(physicalDmg, dmgType, (other.transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);
            AudioManager1.instance.Play(hitsound);
        }
    }

    protected void GetTarget(Collider2D other)
    {
        target = null;

        if (other.isTrigger)
            return;

        target = other.GetComponent<IDamageable>();
        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }
    }

    public void Use()
    {
        if (canAttack && !pressed)
        {
            AudioManager1.instance.Play(iceSound);
            specialParticles.Stop();
            waveParticles.Play();

            canAttack = false;
            pressed = true;

            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius, hitMask);

            for (int i = 0; i < hits.Length; i++)
            {
                HitCollider(hits[i]);
            }

            StartCoroutine(Cooldown());
        }
    }

    public void StopUse()
    {
        pressed = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
        specialParticles.Play();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
