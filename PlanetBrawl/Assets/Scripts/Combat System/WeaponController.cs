using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    //VARIABLES
    #region

    public float damage = 10;
    public float knockback = 500;
    public float stunTime = 0.25f;
    public bool playerWeapon = true;

    protected int playerNr = 1;

    protected Transform weapon; //Transform of THIS game object
    protected Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    protected Rigidbody2D rb2d; //The moons Rigidbody
    protected IDamageable target; //Used to create a reference of a target and hit it

    #endregion

    protected virtual void Start()
    {
        //Initializes everything
        weapon = transform;
        if (playerWeapon)
        {
            origin = weapon.parent;
            playerNr = origin.GetComponentInParent<PlayerController>().playerNr;
        }
        rb2d = GetComponent<Rigidbody2D>();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            //Hit the target if it is damageable
            target.PhysicalHit(damage, (other.transform.position - weapon.position).normalized * knockback, stunTime);
        }
    }
}
