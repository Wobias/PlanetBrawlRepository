using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Speeboost_Pickup : MonoBehaviour
{
    public float speedBoost;
    public float effectTime;

    protected ISpeedable target;


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform.root.GetComponent<ISpeedable>();

        //Hit the target if it is damageable
        if (target != null)
        {
            target.SpeedEffect(speedBoost, effectTime);
            Destroy(gameObject);
        }
    }
}
