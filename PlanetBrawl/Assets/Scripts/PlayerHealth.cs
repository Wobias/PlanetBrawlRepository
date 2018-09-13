using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    public float health = 100f;

    public float testDamage = 10f;

    Transform myTransform;

    Vector3 scaleVector;
    Vector3 minScale = new Vector3(0.6f, 0.6f, 0.6f);
    Vector3 dmgVector;

    // Use this for initialization
    void OnEnable()
    {
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            health -= testDamage;
            DownScaling(testDamage);

        }


    }

    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;

            DownScaling(health);
        }
        else
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("player dead");
    }

    void DownScaling(float scale)
    {
        scaleVector = myTransform.localScale;
        scale /= 100f;

        scaleVector *= scale;
        myTransform.localScale = scaleVector;
        
        if (scaleVector.x < minScale.x)
        {
            scaleVector = minScale;
            myTransform.localScale = scaleVector;
        }
        Debug.Log(myTransform.localScale);
    }
}
