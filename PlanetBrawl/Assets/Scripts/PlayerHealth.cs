using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour,IDamageable
{
    public float health = 100f;

    Transform myTransform;
    Vector3 myVector3;

    // Use this for initialization
    void Start()
    {
        myTransform = GetComponent<Transform>();
        myVector3 = myTransform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (health < 1)
        {
            Die();
        }

        

    }

    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;

            DownScaling();

            Debug.Log(health);
        }
    }

    public void Die()
    {
        Debug.Log("player dead");
    }

    void DownScaling()
    {
        myVector3 *= 0.5f;
        myTransform.localScale = myVector3;
    }

}
