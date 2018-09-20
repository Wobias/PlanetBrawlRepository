using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IDamageable
{
    private Rigidbody2D rb;
    private Transform myTransform;
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    private int whichItem;
    private int whichDirection;

    // Asteroid Health Variable
    public float health = 10f;

    public float asteroidDamage = 10f;

    void Start()
    {
        myTransform = gameObject.transform;
        rb = gameObject.GetComponent<Rigidbody2D>();
        ChooseDirection();

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
        whichItem = Random.Range(1, 14);
        switch (whichItem)
        {
            case 1:
                Instantiate(item1, transform.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(item2, transform.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(item3, transform.position, Quaternion.identity);
                break;
            case 4:
                Instantiate(item4, transform.position, Quaternion.identity);
                break;
        }

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
        if (other.gameObject.GetComponent<IDamageable>() != null)
        {
            other.gameObject.GetComponent<IDamageable>().Hit(asteroidDamage);
            Hit(asteroidDamage);
        }
    }

}
