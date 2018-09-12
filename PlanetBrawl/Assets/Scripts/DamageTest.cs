using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTest : MonoBehaviour
{
    public float dmg = 10f;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.GetComponent<IDamageable>() != null)
        {
            other.GetComponent<IDamageable>().Hit(dmg);
        }

    }
}
