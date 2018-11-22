using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medipack_Pickup : MonoBehaviour
{
    public float healthBonus;
    public string healSound = "healing";

    protected Player_HealthController target;


    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform.root.GetComponent<Player_HealthController>();

        //Hit the target if it is damageable
        if (target != null)
        {
            target.Heal(healthBonus);
            AudioManager1.instance.Play(healSound);
            Destroy(gameObject);
        }
    }
}
