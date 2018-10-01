using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_ShootingStar : WeaponController
{
    //VARIABLES
    #region

    public float shotSpeed = 1;
    public float rotationSpeed = 0.1f; //A value of 1 or higher will make the rotation instant
    public float lifetime = 1;

    private Vector2 direction; //Aiming Direction
    private Quaternion targetRotation; //Quaternion of the Aiming Direction
    private bool shot = false;

    #endregion


    void Update()
    {
        if (shot)
            return;

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

        //Check for a Shot
        if (Input.GetAxisRaw("Fire" + playerNr) == 1)
        {
            rb2d.velocity = -(origin.position - weapon.position).normalized * shotSpeed;
            rb2d.isKinematic = false; //Unlock the moons position
            shot = true;
            weapon.SetParent(null);
            Destroy(origin.gameObject, lifetime);
        }

        #endregion
    }
}
