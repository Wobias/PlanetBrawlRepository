using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_Nuke : MonoBehaviour
{
    public float speed;
    public float rotSpeed;
    [HideInInspector]
    public int playerNr = 1;

    Quaternion targetRotation; //Quaternion of the Aiming Direction
    Rigidbody2D rb2d;


    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb2d.velocity = transform.right * speed;
        Aim(new Vector2(InputSystem.ThumbstickInput(ThumbStick.LeftX, playerNr - 1), InputSystem.ThumbstickInput(ThumbStick.LeftY, playerNr - 1)));
    }

    public void Aim(Vector2 direction)
    {
        if (direction.x != 0 || direction.y != 0)
        {
            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Apply the angle to the target rotation
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        //Rotates the moon(actually the origin) closer to the target rotation depending on the rotation speed
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSpeed * Time.deltaTime);
    }
}
