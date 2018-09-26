using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : WeaponController, IDamageable
{
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject[] itemDrops;
    private int whichItem;
    private int whichDirection;
    private bool canHit = true;

    // Asteroid Health Variable
    public float health = 10f;
    public float selfDamage = 1;


    protected override void Start()
    {
        base.Start();
        ChooseDirection();
    }

    void FixedUpdate()
    {
        weapon.Rotate(Vector3.back * Time.fixedDeltaTime * rotateSpeed);
    }

    //IDamageable method
    public void PhysicalHit(float hitDamage, Vector2 knockbackForce, float stunTime)
    {
        if (!canHit)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        health -= hitDamage;

        if (health <= 0)
        {
            Destroy();
        }

        canHit = false;
        StartCoroutine(AllowHit(stunTime));
    }

    public void Poison(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        if (!canHit)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);
    }

    public void Burn(float dps, float duration, Vector2 knockbackForce, float stunTime)
    {
        if (!canHit)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);
    }

    public void Freeze(float freezeDamage, Vector2 knockbackForce, float stunTime, float thawTime)
    {
        if (!canHit)
            return;

        if (knockbackForce != Vector2.zero)
            rb2d.AddForce(knockbackForce);

        health -= freezeDamage;

        if (health <= 0)
        {
            Destroy();
        }

        canHit = false;
        StartCoroutine(AllowHit(stunTime));
    }

    public void IonDamage(float dps)
    {
        return;
    }

    public void StopIon()
    {
        throw new System.NotImplementedException();
    }

    //Destroy Method
    public void Destroy()
    {
        whichItem = Random.Range(0, itemDrops.Length);
        Instantiate(itemDrops[whichItem], transform.position, Quaternion.identity);
        Destroy(gameObject);
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



    protected override void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            //Hit the target if it is damageable
            target.PhysicalHit(damage, (other.transform.position - weapon.position).normalized * knockback, stunTime);
            PhysicalHit(selfDamage, -(other.transform.position - weapon.position).normalized * knockback, stunTime);
        }
    }

    IEnumerator AllowHit(float timeout)
    {
        yield return new WaitForSeconds(timeout);

        canHit = true;
    }
}
