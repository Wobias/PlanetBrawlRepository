using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Transform myTransform;
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject item;

    // Asteroid Health Variable
    public float health = 10f;

    public float asteroidDamage = 10f;

    void Start()
    {
        myTransform = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(movementSpeed, movementSpeed));
    }

    void Update()
    {
        myTransform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);

    }

    //IDamageable method
    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;
            if(health <= 0)
            {
                Destroy();
            }
        }
        else
        {
            Destroy();
        }
    }

    //Destroy Method
    public void Destroy()
    {
        Destroy(gameObject);
        Instantiate(item, transform.position, Quaternion.identity);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().Hit(asteroidDamage);
            Hit(asteroidDamage);
        }
    }

}
