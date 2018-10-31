using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour, ISpeedable
{
    Rigidbody2D myRigidbody;
    [HideInInspector]
    public Vector2 direction;

    //Speed Variables
    #region
    public float speed = 250f;
    public float speedBonus = 100f;
    public float sprintMultipier = 2f;

    float baseSpeed;
    float fullHpSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    private float sprintSpeed;

    [HideInInspector]
    public bool isSprinting = false;
    #endregion

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //Set Speed Variables
        baseSpeed = speed;
        fullHpSpeed = speed;
        sprintSpeed = baseSpeed * sprintMultipier;
        averageHpSpeed = baseSpeed + speedBonus;
        lowHpSpeed = baseSpeed + speedBonus * 2;
        criticalHpSpeed = baseSpeed + speedBonus * 3;
    }

    void Update()
    {
        //Check for a Sprint
        if (isSprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = baseSpeed;
        }
    }

    void FixedUpdate()
    {
        myRigidbody.AddForce(direction * speed * Time.fixedDeltaTime);
    }

    //ISpeedable Method
    public void SpeedToSize(HealthState currentHealthState)
    {
        if (currentHealthState == HealthState.full)
        {
            speed = fullHpSpeed;
            baseSpeed = fullHpSpeed;
            sprintSpeed = fullHpSpeed * sprintMultipier;
        }
        else if (currentHealthState == HealthState.average)
        {
            speed = averageHpSpeed;
            baseSpeed = averageHpSpeed;
            sprintSpeed = averageHpSpeed * sprintMultipier;
        }
        else if(currentHealthState == HealthState.low)
        {
            speed = lowHpSpeed;
            baseSpeed = lowHpSpeed;
            sprintSpeed = lowHpSpeed * sprintMultipier;
        }
        else if(currentHealthState == HealthState.critical)
        {
            speed = criticalHpSpeed;
            baseSpeed = criticalHpSpeed;
            sprintSpeed = criticalHpSpeed * sprintMultipier;
        }
    }
}
