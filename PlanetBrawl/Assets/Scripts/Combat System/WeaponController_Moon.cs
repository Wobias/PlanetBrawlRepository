using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_Moon : WeaponController
{
    //VARIABLES
    #region

    public float punchSpeed = 1;
    public float retractSpeed = 1; //Should (usually) be slower than punch speed
    public float rotationSpeed = 0.1f; //A value of 1 or higher will make the rotation instant
    public float maxDistance = 5; //Maximum Shot Reach

    private enum MoonState { orbit, shooting, retracting }; //Defines the movement states the moon can be in
    private MoonState moonState = MoonState.orbit; //The actual variable for that
    private bool triggerPressed = false; //Helper Variable because Triggers are an Axis not a button

    private Vector2 direction; //Aiming Direction
    private Quaternion targetRotation; //Quaternion of the Aiming Direction
    private Vector2 minDistance; //Basically the orbit

    #endregion

    protected override void Start()
    {
        base.Start();

        targetRotation = origin.rotation;
        minDistance = weapon.localPosition;
    }

    void Update()
    {
        //AIMING
        #region

        //Get the Aiming Direction
        direction = new Vector2(Input.GetAxis("AimHor" + playerNr), Input.GetAxis("AimVer" + playerNr));

        //Set the target rotation if the direction changed
        if (direction.x != 0 || direction.y != 0)
        {
            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Apply the angle to the target rotation
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
        origin.rotation = Quaternion.Lerp(origin.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        #endregion

        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (!triggerPressed && Input.GetAxisRaw("Fire" + playerNr) == 1 && moonState == MoonState.orbit)
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
            if ((origin.position - weapon.position).magnitude < maxDistance)
            {
                //If the moon is below maxDistance it gets shot further
                rb2d.velocity = -(origin.position - weapon.position).normalized * punchSpeed;
            }
            else
            {
                //If the moon has reached maxDistance it starts retracting
                moonState = MoonState.retracting;
            }
        }
        else if (moonState == MoonState.retracting)
        {
            if ((origin.position - weapon.position).magnitude > Mathf.Abs(minDistance.magnitude))
            {
                //If the moon is above minDistance it keeps retracting
                rb2d.velocity = (origin.position - weapon.position).normalized * retractSpeed;
            }
            else
            {
                //If the moon has reached minDistance stop it and set it to kinematic
                rb2d.velocity = Vector2.zero;
                weapon.position = origin.position + minDistance.y * weapon.up + minDistance.x * weapon.right; //Reset the position in case it overshot the minDistance
                moonState = MoonState.orbit;
                rb2d.isKinematic = true;
            }
        }

        #endregion
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (moonState == MoonState.shooting)
        {
            moonState = MoonState.retracting;
        }
    }
}
