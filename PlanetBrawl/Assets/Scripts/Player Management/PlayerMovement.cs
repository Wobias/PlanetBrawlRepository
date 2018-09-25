using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,ISpeedable
{
    public float speed = 250f;
    public float speedBonus = 100f;
    public float sprintMultipier = 2f;

    float baseSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    private float sprintSpeed;

    private int playerNr = 1;

    public bool isSprinting = false;

    Vector2 direction;
    Rigidbody2D myRigidbody;

    void Start()
    {
        playerNr = GetComponent<PlayerController>().playerNr;

        myRigidbody = GetComponent<Rigidbody2D>();

        //Set Speed Variables
        baseSpeed = speed;
        sprintSpeed = baseSpeed * sprintMultipier;
        averageHpSpeed = baseSpeed + speedBonus;
        lowHpSpeed = baseSpeed + speedBonus * 2;
        criticalHpSpeed = baseSpeed + speedBonus * 3;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.T))
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            speed = baseSpeed;
            isSprinting = false;
        }
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
            baseSpeed = averageHpSpeed;
            sprintSpeed = averageHpSpeed * sprintMultipier;
        }
        if (currentHealthState == HealthState.low)
        {
            speed = lowHpSpeed;
            baseSpeed = lowHpSpeed;
            sprintSpeed = lowHpSpeed * sprintMultipier;
        }
        if (currentHealthState == HealthState.critical)
        {
            speed = criticalHpSpeed;
            baseSpeed = criticalHpSpeed;
            sprintSpeed = criticalHpSpeed * sprintMultipier;
        }
    }
}
