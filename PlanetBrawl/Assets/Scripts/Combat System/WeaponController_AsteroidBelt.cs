using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_AsteroidBelt : WeaponController
{
    //VARIABLES
    #region

    public float slowRotSpeed = 5; //A value of 1 or higher will make the rotation instant
    public float fastRotSpeed = 15;
    public float punchSpeed = 25;
    public float escapeTime = 2;
    public float lifetime = 1;

    private enum AsteroidState { orbit, speedOrbit, escaped }; //Defines the movement states the moon can be in
    private AsteroidState asteroidState = AsteroidState.orbit; //The actual variable for that
    private float escapeCounter = 0;

    #endregion

    void Update()
    {
        if (asteroidState == AsteroidState.escaped)
            return;

        //CHECKING FOR INPUT
        #region

        //Check for a Punch
        if (Input.GetAxisRaw("Fire" + playerNr) == 1)
        {
            asteroidState = AsteroidState.speedOrbit;
            //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
            origin.Rotate(new Vector3(0, 0, -fastRotSpeed * Time.deltaTime));
            escapeCounter += Time.deltaTime;

            if (escapeCounter >= escapeTime)
            {
                rb2d.isKinematic = false;
                rb2d.velocity = -weapon.up * punchSpeed;
                asteroidState = AsteroidState.escaped;
                weapon.SetParent(null);
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
}
