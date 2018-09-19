using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_AsteroidBelt : MonoBehaviour
{
    //VARIABLES
    #region

    public int player = 1;
    public float damage = 10;
    public float knockback = 500;
    public float hitTimeout = 0.25f; //The amount of time the moon can't damage anything after a hit
    public float slowRotSpeed = 5; //A value of 1 or higher will make the rotation instant
    public float fastRotSpeed = 15;
    public float punchSpeed = 25;
    public float escapeTime = 2;
    public float lifetime = 1;

    private bool canHit = true; //Determines if the moon can do damage at the moment
    private enum AsteroidState { orbit, speedOrbit, escaped }; //Defines the movement states the moon can be in
    private AsteroidState asteroidState = AsteroidState.orbit; //The actual variable for that
    private float escapeCounter = 0;

    private Rigidbody2D rb2d; //The moons Rigidbody
    private Transform asteroid; //Transform of THIS game object
    private Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    private IDamageable target; //Used to create a reference of a target and hit it
    private Rigidbody2D targetRB;

    #endregion

    void Start()
    {
        //Initializes everything
        asteroid = transform;
        origin = asteroid.parent;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (asteroidState == AsteroidState.escaped)
            return;

        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (Input.GetAxisRaw("Fire" + player) == -1)
        {
            asteroidState = AsteroidState.speedOrbit;
            //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
            origin.Rotate(new Vector3(0, 0, -fastRotSpeed));
            escapeCounter += Time.deltaTime;

            if (escapeCounter >= escapeTime)
            {
                rb2d.isKinematic = false;
                rb2d.velocity = -asteroid.up * punchSpeed;
                asteroidState = AsteroidState.escaped;
                asteroid.SetParent(null);
                Destroy(origin.gameObject, lifetime);
            }
        }
        else
        {
            if (asteroidState == AsteroidState.speedOrbit)
            {
                asteroidState = AsteroidState.orbit;
                escapeCounter = 0;
            }

            origin.Rotate(new Vector3(0, 0, -slowRotSpeed));
        }

        #endregion
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        target = other.GetComponent<IDamageable>();
        targetRB = other.GetComponent<Rigidbody2D>();

        if (target != null && canHit)
        {
            if (targetRB != null)
            {
                targetRB.AddForce((other.transform.position - asteroid.position).normalized * knockback);
            }
            else
            {
                targetRB = other.transform.parent.GetComponent<Rigidbody2D>();

                if (targetRB != null)
                {
                    targetRB.AddForce((other.transform.position - asteroid.position).normalized * knockback);
                }
            }

            //Hit the target if it is damageable
            target.Hit(damage);
            canHit = false;
            StartCoroutine(EnableHit());
        }
    }

    IEnumerator EnableHit()
    {
        yield return new WaitForSeconds(hitTimeout);

        canHit = true;
    }
}
