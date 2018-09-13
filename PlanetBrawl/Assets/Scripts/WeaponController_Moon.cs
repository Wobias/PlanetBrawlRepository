using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_Moon : MonoBehaviour
{
    //VARIABLES
    #region

    public int player = 1;
    public float damage = 10;
    public float hitTimeout = 0.25f; //The amount of time the moon can't damage anything after a hit
    public float punchSpeed = 1;
    public float retractSpeed = 1; //Should (usually) be slower than punch speed
    public float rotationSpeed = 0.1f; //A value of 1 or higher will make the rotation instant
    public float minDistance = 1; //Basically the orbit
    public float maxDistance = 5; //Maximum Shot Reach

    private enum MoonState { orbit, shooting, retracting }; //Defines the movement states the moon can be in
    private MoonState moonState = MoonState.orbit; //The actual variable for that
    private bool triggerPressed = false; //Helper Variable because Triggers are an Axis not a button
    private bool canHit = true; //Determines if the moon can do damage at the moment

    private Vector2 direction; //Aiming Direction
    private Quaternion targetRotation; //Quaternion of the Aiming Direction
    private Transform moon; //Transform of THIS game object
    private Transform origin; //Transform of the PARENT that is responsible for rotating the moon
    private Rigidbody2D rb2d; //The moons Rigidbody
    private IDamageable target; //Used to create a reference of a target and hit it

    #endregion

    void Start()
    {
        //Initializes everything
        moon = transform;
        origin = moon.parent;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //AIMING
        #region

        //Get the Aiming Direction
        direction = new Vector2(Input.GetAxis("AimHor" + player), Input.GetAxis("AimVer" + player));

        //Set the target rotation if the direction changed
        if (direction.x != 0 || direction.y != 0)
        {
            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Apply the angle to the target rotation
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
        origin.rotation = Quaternion.Lerp(origin.rotation, targetRotation, rotationSpeed);

        #endregion

        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (!triggerPressed && Input.GetAxisRaw("Fire" + player) == -1 && moonState == MoonState.orbit)
        {
            triggerPressed = true;
            moonState = MoonState.shooting;
            rb2d.isKinematic = false; //Unlock the moons position
        }

        //Check for Trigger Release
        if (triggerPressed && Input.GetAxisRaw("Fire" + player) == 0)
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
                rb2d.velocity = -(origin.position - moon.position).normalized * retractSpeed;
            }
            else
            {
                //If the moon has reached maxDistance it starts retracting
                moonState = MoonState.retracting;
            }
        }
        else if (moonState == MoonState.retracting)
        {
            if ((origin.position - moon.position).magnitude > minDistance)
            {
                //If the moon is above minDistance it keeps retracting
                rb2d.velocity = (origin.position - moon.position).normalized * retractSpeed;
            }
            else
            {
                //If the moon has reached minDistance stop it and set it to kinematic
                rb2d.velocity = Vector2.zero;
                moon.position = origin.position + minDistance * moon.right; //Reset the position in case it overshot the minDistance
                moonState = MoonState.orbit;
                rb2d.isKinematic = true;
            }
        }

        #endregion
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Make a reference to the target
        IDamageable target = other.GetComponent<IDamageable>();

        if (target != null && canHit)
        {
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
