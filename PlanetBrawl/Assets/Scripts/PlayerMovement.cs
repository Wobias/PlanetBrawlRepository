using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,ISpeedable
{
    public float speed = 500f;
    public float speedBonus = 50f;

    float baseSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    private int playerNr = 1;

    Vector2 direction;
    Rigidbody2D myRigidbody;

    void Start()
    {
        playerNr = GetComponent<PlayerController>().playerNr;

        myRigidbody = GetComponent<Rigidbody2D>();

        //Set Speed Variables
        baseSpeed = speed;
        averageHpSpeed = baseSpeed + speedBonus;
        lowHpSpeed = baseSpeed + speedBonus * 2;
        criticalHpSpeed = baseSpeed + speedBonus * 3;
    }

    void FixedUpdate()
    {
        direction.x = Input.GetAxis("Horizontal" + playerNr);
        direction.y = Input.GetAxis("Vertical" + playerNr);
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
