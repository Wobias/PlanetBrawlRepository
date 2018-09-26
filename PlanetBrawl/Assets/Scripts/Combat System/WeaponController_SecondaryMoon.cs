using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_SecondaryMoon : MonoBehaviour
{
    //VARIABLES
    #region

    public float damage = 10;
    public float knockback = 500;
    public float stunTime = 0.25f; //The amount of time the moon can't damage anything after a hit
    public float punchSpeed = 1;
    public float retractSpeed = 1; //Should (usually) be slower than punch speed
    public float maxDistance = 5; //Maximum Shot Reach

    private enum MoonState { orbit, shooting, retracting }; //Defines the movement states the moon can be in
    private MoonState moonState = MoonState.orbit; //The actual variable for that
    private bool triggerPressed = false; //Helper Variable because Triggers are an Axis not a button
    private int playerNr = 1;

    private Transform moon; //Transform of THIS game object
    private Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    private Rigidbody2D rb2d; //The moons Rigidbody
    private IDamageable target; //Used to create a reference of a target and hit it
    private Vector2 minDistance; //Basically the orbit

    #endregion

    void Start()
    {
        //Initializes everything
        moon = transform;
        origin = moon.parent;
        playerNr = origin.GetComponentInParent<PlayerController>().playerNr;
        rb2d = GetComponent<Rigidbody2D>();

        minDistance = moon.localPosition;
    }

    void Update()
    {
        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (!triggerPressed && Input.GetAxisRaw("Fire" + playerNr) == -1 && moonState == MoonState.orbit)
        {
            triggerPressed = true;
            moonState = MoonState.shooting;
            rb2d.isKinematic = false; //Unlock the moons position
        }

        //Check for Trigger Release
        if (triggerPressed && Input.GetAxisRaw("Fire" + playerNr) == 0)
        {
            triggerPressed = false;

            //In case the moon is mid-punch, retract it
            if (moonState == MoonState.shooting)
            {
                moonState = MoonState.retracting;
            }
        }

        #endregion

        //UPDATE MOON STATE & POSITION
        #region

        if (moonState == MoonState.shooting)
        {
            if ((origin.position - moon.position).magnitude < maxDistance)
            {
                //If the moon is below maxDistance it gets shot further
                rb2d.velocity = -(origin.position - moon.position).normalized * punchSpeed;
            }
            else
            {
                //If the moon has reached maxDistance it starts retracting
                moonState = MoonState.retracting;
            }
        }
        else if (moonState == MoonState.retracting)
        {
            if ((origin.position - moon.position).magnitude > Mathf.Abs(minDistance.magnitude))
            {
                //If the moon is above minDistance it keeps retracting
                rb2d.velocity = (origin.position - moon.position).normalized * retractSpeed;
            }
            else
            {
                //If the moon has reached minDistance stop it and set it to kinematic
                rb2d.velocity = Vector2.zero;
                moon.position = origin.position + minDistance.y * moon.up + minDistance.x * moon.right; //Reset the position in case it overshot the minDistance
                moonState = MoonState.orbit;
                rb2d.isKinematic = true;
            }
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
            target.PhysicalHit(damage, (other.transform.position - moon.position).normalized * knockback, stunTime);
        }

        if (moonState == MoonState.shooting)
        {
            moonState = MoonState.retracting;
        }
    }
}
