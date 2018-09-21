using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Transform myTransform;
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject[] itemDrops;
    private int whichItem;
    private int whichDirection;
    private IDamageable target;
    private Rigidbody2D targetRB;

    // Asteroid Health Variable
    public float health = 10f;

    public float asteroidDamage = 10f;
    public float selfDamage = 5;
    public float knockback = 500;

    void Start()
    {
        myTransform = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ChooseDirection();

    }

    void FixedUpdate()
    {
        myTransform.Rotate(Vector3.back * Time.fixedDeltaTime * rotateSpeed);



    }

    //IDamageable method
    public void Hit(float damage)
    {
        if (health >= 1)
        {
            health -= damage;
            if (health <= 0)
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
        whichItem = Random.Range(0, itemDrops.Length);
        Instantiate(itemDrops[whichItem], transform.position, Quaternion.identity);
    }

    public void ChooseDirection()
    {
        //whichDirection = Random.Range(1, 4);
        //Debug.Log(whichDirection);
        //switch (whichDirection)
        //{
        //    case 1:
        //        rb.AddForce(new Vector2(movementSpeed, movementSpeed)); // Right UP
        //        break;
        //    case 2:
        //        rb.AddForce(transform.up * movementSpeed); // Up
        //        break;
        //    case 3:
        //        rb.AddForce(-transform.up * movementSpeed); // Down
        //        break;
        //    case 4:
        //        rb.AddForce(new Vector2(-movementSpeed, -movementSpeed)); // Left down
        //        break;
        //}
        int whichBorder = GameObject.Find("Spawner").GetComponent<AsteroidSpawner>().whichBorder;
        if (whichBorder == 1)
        {
            rb.AddForce(new Vector2(movementSpeed, movementSpeed)); // Right UP
        }
        else if(whichBorder == 2)
        {
            rb.AddForce(-transform.up * movementSpeed); // Down
        }
        else if (whichBorder == 3)
        {
            rb.AddForce(new Vector2(-movementSpeed, movementSpeed)); // Left Up
        }
        else if (whichBorder == 4)
        {
            rb.AddForce(transform.up * movementSpeed); //UP
        }
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        target = other.GetComponent<IDamageable>();
        targetRB = other.GetComponent<Rigidbody2D>();

        if (target != null)
        {
            if (targetRB != null)
            {
                targetRB.AddForce((other.transform.position - myTransform.position).normalized * knockback);
            }
            else
            {
                targetRB = other.transform.parent.GetComponent<Rigidbody2D>();

                if (targetRB != null)
                {
                    targetRB.AddForce((other.transform.position - myTransform.position).normalized * knockback);
                }
            }

            //Hit the target if it is damageable
            target.Hit(asteroidDamage);
            Hit(selfDamage);
        }
    }

}
