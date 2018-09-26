using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour,ISpeedable
{
    private int playerNr = 1;

    Transform myTransform;
    Rigidbody2D myRigidbody;
    Vector2 direction;

    //Speed Variables
    #region
    public float speed = 250f;
    public float speedBonus = 100f;
    public float sprintMultipier = 2f;

    float baseSpeed;
    float averageHpSpeed;
    float lowHpSpeed;
    float criticalHpSpeed;

    private float sprintSpeed;

    public bool isSprinting = false;
    #endregion

    // Ram Variables
    #region
    public float damage = 10;
    public float sprintDmgMultiplier = 2;
    public float knockback = 250;
    public float stunTime = 0.25f; //The amount of time the moon can't damage anything after a hit
    private IDamageable target; //Used to create a reference of a target and hit it
    #endregion

    void Start()
    {
        myTransform = GetComponent<Transform>();

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
        //Check for a Sprint
        if (!isSprinting && Input.GetAxisRaw("Sprint" + playerNr) == 1)
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        //Check for Trigger Release
        if (isSprinting && Input.GetAxisRaw("Sprint" + playerNr) == 0)
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Make a reference to the target
        target = collision.collider.GetComponent<IDamageable>();

        if (target != null)
        {
        //Hit the target if it is damageable

            if (isSprinting)
            {
                target.PhysicalHit(damage * sprintDmgMultiplier, (collision.gameObject.transform.position - myTransform.position).normalized * (knockback * sprintDmgMultiplier), stunTime);
            }
            else
            {
                
                target.PhysicalHit(damage, (collision.gameObject.transform.position - myTransform.position).normalized * knockback, stunTime);
            }

        }
    }
}
