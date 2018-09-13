using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int player = 1;
    public static float speed = 500f;

    Vector2 direction;
    Rigidbody2D myRigidbody;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal" + player);
        direction.y = Input.GetAxis("Vertical" + player);
        myRigidbody.AddForce(direction * speed * Time.fixedDeltaTime);
    }
}
