using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject[] itemDrops;
    private int whichItem;
    private int whichDirection;
    private Rigidbody2D rb2d;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ChooseDirection();
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.back * Time.fixedDeltaTime * rotateSpeed);
    }

    //Destroy Method
    public void Destroy()
    {
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
        int whichBorder = FindObjectOfType<AsteroidSpawner>().whichBorder;
        if (whichBorder == 1)
        {
            rb2d.AddForce(new Vector2(movementSpeed, movementSpeed)); // Right UP
        }
        else if(whichBorder == 2)
        {
            rb2d.AddForce(-transform.up * movementSpeed); // Down
        }
        else if (whichBorder == 3)
        {
            rb2d.AddForce(new Vector2(-movementSpeed, movementSpeed)); // Left Up
        }
        else if (whichBorder == 4)
        {
            rb2d.AddForce(transform.up * movementSpeed); //UP
        }
    }
}
