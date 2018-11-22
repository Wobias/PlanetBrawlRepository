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
    public DamageType dmgType = DamageType.physical;
    [SerializeField]
    public float knockback;
    [SerializeField]
    public float stunTime;
    [SerializeField]
    public float effectTime;

    public bool destroyOnHit = false;

    protected IDamageable target;
    protected WeaponController weapon;
    protected int playerNr = 0;
    protected bool isWeapon = false;

    protected DamageType buffType = DamageType.none;
    protected float buffTime = 0;
    protected bool gotBuff = false;


    protected virtual void Start()
    {
        weapon = GetComponentInParent<WeaponController>();

        if (weapon != null)
            isWeapon = true;

        PlayerController controller = transform.root.GetComponent<PlayerController>();
        if (controller != null)
            playerNr = controller.playerNr;
    }

    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (!this.enabled)
            return;

        GetTarget(col.collider);
        
        //Hit the target if it is damageable
        if (target != null)
        {
            if (gotBuff)
            {
                if (buffType == DamageType.physical)
                {
                    physicalDmg *= 2;
                    knockback *= 2;
                }
                else
                {
                    target.Hit(0, buffType, Vector2.zero, 0, playerNr, buffTime);
                    weapon.RemoveElement();
                }
            }

            if (isWeapon)
            {
                target.Hit(physicalDmg, dmgType, (col.transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);
                weapon.OnHit();
            }
            else
            {
                target.Hit(physicalDmg, dmgType, (col.transform.position - transform.position).normalized * knockback, stunTime, playerNr, effectTime);
            }
            AudioManager1.instance.Play(hitsound);

            if (gotBuff && buffType == DamageType.physical)
            {
                physicalDmg /= 2;
                knockback /= 2;
                weapon.RemoveElement();
            }
        }
        else if(isWeapon)
        {
            weapon.OnHit();
        }

        if (destroyOnHit)
            Destroy(gameObject);
    }

    public void AddBuff(DamageType buffType, float buffTime)
    {
        this.buffType = buffType;
        this.buffTime = buffTime;
        gotBuff = true;
    }

    public void RemoveBuff()
    {
        gotBuff = false;
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
