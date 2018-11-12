using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_ContactDamage : MonoBehaviour
{
    //public bool useWeaponStats = true;

    public string hitsound = "punch";

    [SerializeField]
    public float physicalDmg;
    [SerializeField]
    public float effectDps;
    [SerializeField]
    public DamageType dmgType = DamageType.physical;
    [SerializeField]
    public float knockback;
    [SerializeField]
    public float stunTime;
    [SerializeField]
    public float effectTime;

    protected IDamageable target;
    protected WeaponController weapon;
    protected bool isWeapon = false;


    protected virtual void Start()
    {
        weapon = GetComponentInParent<WeaponController>();

        if (weapon != null)
            isWeapon = true;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (!this.enabled)
            return;

        GetTarget(col.collider);
        
        //Hit the target if it is damageable
        if (target != null)
        {
            if (isWeapon)
            {
                target.Hit(physicalDmg, effectDps, dmgType, (col.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
                weapon.OnHit();
            }
            else
            {
                target.Hit(physicalDmg, effectDps, dmgType, (col.transform.position - transform.position).normalized * knockback, stunTime, effectTime);
            }
            AudioManager1.instance.Play(hitsound);
        }
        else if(isWeapon)
        {
            weapon.OnHit();
        }
    }

    protected void GetTarget(Collider2D other)
    {
        target = null;
        target = other.GetComponent<IDamageable>();
        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }
    }
}
