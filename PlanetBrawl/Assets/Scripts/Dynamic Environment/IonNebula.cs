using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonNebula : MonoBehaviour
{
    public float dps = 20;

    private IDamageable target;


    void OnTriggerEnter2D(Collider2D other)
    {
        target = other.GetComponent<IDamageable>();

        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }

        if (target != null)
        {
            target.IonDamage(dps);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        target = other.GetComponent<IDamageable>();

        if (target == null)
        {
            target = other.attachedRigidbody?.GetComponent<IDamageable>();
        }

        if (target != null)
        {
            target.StopIon();
        }
    }
}
