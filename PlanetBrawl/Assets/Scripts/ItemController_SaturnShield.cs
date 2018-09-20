using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController_SaturnShield : MonoBehaviour, IDamageable
{
    public float health;

    private PlayerHealth playerHealth;


    void Start()
    {
        playerHealth = GetComponentInParent<PlayerHealth>();
        gameObject.layer = transform.parent.gameObject.layer;

        if (playerHealth != null)
            playerHealth.enabled = false;
    }

    public void Hit(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            if (playerHealth != null)
                playerHealth.enabled = true;

            Destroy(gameObject);
        }
    }
}
