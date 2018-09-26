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
    public float knockback = 500;
    public float hitTimeout = 0.25f; //The amount of time the moon can't damage anything after a hit
    private IDamageable target; //Used to create a reference of a target and hit it
    private Rigidbody2D targetRB;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Make a reference to the target
        target = collision.gameObject.GetComponent<IDamageable>();
        targetRB = collision.gameObject.GetComponent<Rigidbody2D>();

        if (target != null)
        {
            if (targetRB != null)
            {
                targetRB.AddForce((collision.gameObject.transform.position - myTransform.position).normalized * knockback);
            }
            else
            {
                targetRB = collision.gameObject.transform.parent.GetComponent<Rigidbody2D>();

                if (targetRB != null)
                {
                    targetRB.AddForce((collision.gameObject.transform.position - myTransform.position).normalized * knockback);
                }
            }

            //Hit the target if it is damageable
            target.Hit(damage);
            StartCoroutine(EnableHit());
        }
    }

    IEnumerator EnableHit()
    {
        yield return new WaitForSeconds(hitTimeout);

    }
}
