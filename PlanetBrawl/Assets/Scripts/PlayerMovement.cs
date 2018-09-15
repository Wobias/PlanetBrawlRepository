using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,ISpeedable
{
    public int player = 1;

    public float speed = 500f;

    float baseSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    Vector2 direction;
    Rigidbody2D myRigidbody;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //Set Speed Variables
        baseSpeed = speed;
        averageHpSpeed = baseSpeed + 50f;
        lowHpSpeed = baseSpeed + 100f;
        criticalHpSpeed = baseSpeed + 150f;
    }

    void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal" + player);
        direction.y = Input.GetAxis("Vertical" + player);
        myRigidbody.AddForce(direction * speed * Time.fixedDeltaTime);
    }

    //ISpeedable Method
    public void SpeedToSize(HealthState currentHealthState)
    {
        if (currentHealthState == HealthState.average)
        {
            speed = averageHpSpeed;
        }
        if (currentHealthState == HealthState.low)
        {
            speed = lowHpSpeed;
        }
        if (currentHealthState == HealthState.critical)
        {
            speed = criticalHpSpeed;
        }
    }
}
