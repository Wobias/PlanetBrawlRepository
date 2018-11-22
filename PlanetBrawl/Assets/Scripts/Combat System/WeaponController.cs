using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OrbitState { orbit, shooting, retracting }; //Defines the movement states the moon can be in

public class WeaponController : MonoBehaviour
{
    //VARIABLES
    #region

    public bool canAttack = true;
    public float rotationSpeed = 0.1f; //A value of 1 or higher will make the rotation instant

    protected Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    protected IDamageable target; //Used to create a reference of a target and hit it
    protected Quaternion targetRotation; //Quaternion of the Aiming Direction
    [SerializeField]
    protected Rigidbody2D[] weaponParts;
    protected Collider2D[] weaponColliders;
    protected Vector2[] weaponMinPos;
    protected Vector2[] weaponInitPos;
    protected Transform charTrans;


    protected Rigidbody2D rb2d; //dummy for no errors plz

    #endregion


    protected virtual void Start()
    {
        //Initializes everything
        origin = transform.parent;
        charTrans = origin.parent.GetComponentInChildren<Player_ContactDamage>().transform;
        targetRotation = origin.rotation;
        //playerNr = origin.GetComponentInParent<PlayerController>().playerNr;

        weaponInitPos = new Vector2[weaponParts.Length];
        weaponMinPos = new Vector2[weaponParts.Length];
        weaponColliders = new Collider2D[weaponParts.Length];
        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponInitPos[i] = weaponParts[i].transform.localPosition;
            weaponMinPos[i] = weaponInitPos[i];
            weaponColliders[i] = weaponParts[i].GetComponent<Collider2D>();
            weaponColliders[i].enabled = false;
        }

        //ResetMinPos();
    }

    protected virtual void OnEnable()
    {
        if (origin)
        {
            targetRotation = origin.rotation;
        }
    }

    public virtual void Aim(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Apply the angle to the target rotation
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (origin)
        {
            //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
            origin.rotation = Quaternion.Slerp(origin.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public virtual bool Shoot(bool isFirePressed)
    {
        return true;
    }

    public virtual void OnHit()
    {
        return;
    }

    public void ResetMinPos()
    {
        for (int i = 0; i < weaponMinPos.Length; i++)
        {
            weaponMinPos[i] = weaponInitPos[i] * charTrans.localScale.x;

            if (weaponParts[i].isKinematic)
            {
                weaponParts[i].transform.localPosition = weaponMinPos[i];
            }
        }

        OnDistanceReset();
    }

    protected virtual void OnDistanceReset()
    {
        return;
    }

    public virtual void ApplyElement(DamageType type, float effectTime)
    {
        RemoveElement();

        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponParts[i].gameObject.GetComponent<Weapon_ContactDamage>()?.AddBuff(type, effectTime);
        }
    }

    public virtual void RemoveElement()
    {
        for (int i = 0; i < weaponParts.Length; i++)
        {
            weaponParts[i].gameObject.GetComponent<Weapon_ContactDamage>()?.RemoveBuff();
        }
    }
}
