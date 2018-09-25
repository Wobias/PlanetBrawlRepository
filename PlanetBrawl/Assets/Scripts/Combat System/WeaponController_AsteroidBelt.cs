using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_AsteroidBelt : MonoBehaviour
{
    //VARIABLES
    #region

    public float damage = 10;
    public float knockback = 500;
    public float stunTime = 0.25f; //The amount of time the moon can't damage anything after a hit
    public float slowRotSpeed = 5; //A value of 1 or higher will make the rotation instant
    public float fastRotSpeed = 15;
    public float punchSpeed = 25;
    public float escapeTime = 2;
    public float lifetime = 1;

    private enum AsteroidState { orbit, speedOrbit, escaped }; //Defines the movement states the moon can be in
    private AsteroidState asteroidState = AsteroidState.orbit; //The actual variable for that
    private float escapeCounter = 0;
    private int playerNr = 1;

    private Rigidbody2D rb2d; //The moons Rigidbody
    private Transform asteroid; //Transform of THIS game object
    private Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    private IDamageable target; //Used to create a reference of a target and hit it

    #endregion

    void Start()
    {
        //Initializes everything
        asteroid = transform;
        origin = asteroid.parent;
        playerNr = origin.GetComponentInParent<PlayerController>().playerNr;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (asteroidState == AsteroidState.escaped)
            return;

        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (Input.GetAxisRaw("Fire" + playerNr) == -1)
        {
            asteroidState = AsteroidState.speedOrbit;
            //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
            origin.Rotate(new Vector3(0, 0, -fastRotSpeed * Time.deltaTime));
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

            origin.Rotate(new Vector3(0, 0, -slowRotSpeed * Time.deltaTime));
        }

        #endregion
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        target = other.GetComponent<IDamageable>();

        if (target != null)
        {
            //Hit the target if it is damageable
            target.PhysicalHit(damage, (other.transform.position - asteroid.position).normalized * knockback, stunTime);
        }
    }
}
