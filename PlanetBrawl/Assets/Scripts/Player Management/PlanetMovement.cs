using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetMovement : MonoBehaviour, ISpeedable, IMovable
{
    Rigidbody2D myRigidbody;
    [HideInInspector]
    public Vector2 direction;

    //Speed Variables
    #region
    public float speed = 250f;
    public float speedBonus = 100f;
    public float effectMultiplier = 1;
    //public float sprintMultipier = 2f;

    public float inputRolloff = 0.1f;
    public float forceRolloff = 0.1f;
    public bool stunned = false;

    float baseSpeed;
    float fullHpSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    public bool invertedMove = false;

    //private float sprintSpeed;
    private Vector2 externalForce = Vector2.zero;
    private Vector2 targetExForce = Vector2.zero;
    private Vector2 gravForce = Vector2.zero;
    private Vector2 targetGravForce = Vector2.zero;

    private Vector2 oldDir;

    private bool canStop = true;

    //[HideInInspector]
    //public bool isSprinting = false;
    #endregion

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        //Set Speed Variables
        baseSpeed = speed;
        fullHpSpeed = speed;
        //sprintSpeed = baseSpeed * sprintMultipier;
        averageHpSpeed = baseSpeed + speedBonus;
        lowHpSpeed = baseSpeed + speedBonus * 2;
        criticalHpSpeed = baseSpeed + speedBonus * 3;
    }

    //void Update()
    //{
    //    //Check for a Sprint
    //    if (isSprinting)
    //    {
    //        speed = sprintSpeed;
    //    }
    //    else
    //    {
    //        speed = baseSpeed;
    //    }
    //}

    void FixedUpdate()
    {
        //myRigidbody.AddForce(direction * speed * effectMultiplier * Time.fixedDeltaTime);
        externalForce = Vector2.Lerp(externalForce, targetExForce, forceRolloff);
        gravForce = Vector2.Lerp(gravForce, targetGravForce, forceRolloff);

        if (!invertedMove)
            myRigidbody.velocity = direction * speed * effectMultiplier * Time.fixedDeltaTime + externalForce + gravForce;
        else
            myRigidbody.velocity = -direction * speed * effectMultiplier * Time.fixedDeltaTime + externalForce + gravForce;

        if (!canStop)
        {
            if (Mathf.Abs(direction.magnitude) < 0.5f)
            {
                myRigidbody.velocity += oldDir * speed * effectMultiplier * Time.deltaTime;
            }

            if (Mathf.Abs(direction.magnitude) >= 0.5f)
            {
                oldDir = direction * 2;
            }
        }
    }

    //ISpeedable Method
    public void SpeedToSize(HealthState currentHealthState)
    {
        if (currentHealthState == HealthState.full)
        {
            speed = fullHpSpeed;
            baseSpeed = fullHpSpeed;
            //sprintSpeed = fullHpSpeed * sprintMultipier;
        }
        else if (currentHealthState == HealthState.average)
        {
            speed = averageHpSpeed;
            baseSpeed = averageHpSpeed;
            //sprintSpeed = averageHpSpeed * sprintMultipier;
        }
        else if(currentHealthState == HealthState.low)
        {
            speed = lowHpSpeed;
            baseSpeed = lowHpSpeed;
            //sprintSpeed = lowHpSpeed * sprintMultipier;
        }
        else if(currentHealthState == HealthState.critical)
        {
            speed = criticalHpSpeed;
            baseSpeed = criticalHpSpeed;
            //sprintSpeed = criticalHpSpeed * sprintMultipier;
        }
    }

    public void SpeedEffect(float speedMultiplier, float timeout=0)
    {
        float newMultiplier = effectMultiplier + speedMultiplier;

        if (newMultiplier > 0)
        {
            effectMultiplier = newMultiplier;

            if (timeout > 0)
            {
                StartCoroutine(StopSpeedEffect(timeout, speedMultiplier));
            }
        }
    }

    public void ApplyGravForce(Vector2 force)
    {
        targetGravForce += force;
    }

    public void ApplyTempExForce(Vector2 force, float time)
    {
        StartCoroutine(AddExForce(force, time));
    }

    void OnDisable()
    {
        direction = Vector2.zero;
    }

    IEnumerator AddExForce(Vector2 force, float time)
    {
        targetExForce += force;
        externalForce = targetExForce;
        yield return new WaitForSeconds(time);
        targetExForce -= force;
    }

    IEnumerator StopSpeedEffect(float timeout, float bonusSpeed)
    {
        yield return new WaitForSeconds(timeout);
        effectMultiplier -= bonusSpeed;
    }

    public void FlushGravForce()
    {
        targetGravForce = Vector2.zero;
    }

    public void FlushVelocity()
    {
        direction = Vector2.zero;
    }

    public void SetBrakes(bool active)
    {
        canStop = active;
    }
}
