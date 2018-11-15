using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element_Pickup : MonoBehaviour
{
    public float effectTime;
    public DamageType elementType;

    void OnTriggerEnter2D(Collider2D other)
    {
        IPlanet target = other.transform.root.GetComponent<IPlanet>();

        if (target != null)
        {
            target.BoostWeapon(elementType, effectTime);
            Destroy(gameObject);
        }
    }
}
