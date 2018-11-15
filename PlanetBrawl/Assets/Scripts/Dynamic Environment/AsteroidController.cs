using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour, IMovable
{
    public float rotateSpeed = 50f;
    public float movementSpeed = 10f;
    public GameObject[] itemDrops;
    private int whichItem;
    private int whichDirection;
    private Vector2 direction;
    private Rigidbody2D rb2d;

    public float inputRolloff = 0.1f;
    public float forceRolloff = 0.1f;

    public string asteroidSound = "asteroidDestruction";

    private Vector2 externalForce = Vector2.zero;
    private Vector2 targetExForce = Vector2.zero;
    private Vector2 gravForce = Vector2.zero;
    private Vector2 targetGravForce = Vector2.zero;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        ChooseDirection();
    }

    void FixedUpdate()
    {
        externalForce = Vector2.Lerp(externalForce, targetExForce, forceRolloff);
        gravForce = Vector2.Lerp(gravForce, targetGravForce, forceRolloff);

        transform.Rotate(Vector3.back * Time.fixedDeltaTime * rotateSpeed);
        rb2d.velocity = externalForce + gravForce;
    }

    //Destroy Method
    public void Destroy()
    {
        whichItem = Random.Range(0, itemDrops.Length);
        AudioManager1.instance.Play(asteroidSound);
        //Instantiate(itemDrops[whichItem], transform.position, Quaternion.identity);
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
            direction = Vector2.right; // Right UP
        }
        else if(whichBorder == 2)
        {
            direction  = Vector2.down; // Down
        }
        else if (whichBorder == 3)
        {
            direction = new Vector2(-1,1); // Left Up
        }
        else if (whichBorder == 4)
        {
            direction = new Vector2(0, 1); //UP
        }

        targetExForce = direction * movementSpeed;
        externalForce = targetExForce;
    }

    public void ApplyGravForce(Vector2 force)
    {
        targetGravForce += force;
    }

    public void FlushGravForce()
    {
        targetGravForce = Vector2.zero;
    }

    public void ApplyTempExForce(Vector2 force, float time)
    {
        StartCoroutine(AddExForce(force, time));
    }

    IEnumerator AddExForce(Vector2 force, float time)
    {
        targetExForce += force;
        externalForce = targetExForce;
        yield return new WaitForSeconds(time);
        targetExForce -= force;
    }
}
