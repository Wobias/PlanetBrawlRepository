using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public float maxDistance = 0.1f;
    public float lookSpeed = 0.25f;

    private Vector2 direction;
    private Transform face;
    private int playerNr = 1;


    void Start()
    {
        face = transform;

        playerNr = face.GetComponentInParent<PlayerController>().playerNr;
    }

    void Update ()
    {
        //AIMING
        #region

        //Get the Aiming Direction
        direction = new Vector2(InputSystem.ThumbstickInput(ThumbSticks.RightX, playerNr-1), InputSystem.ThumbstickInput(ThumbSticks.RightY, playerNr - 1));

        if (direction == Vector2.zero)
        {
            direction = new Vector2(InputSystem.ThumbstickInput(ThumbSticks.LeftX, playerNr - 1), InputSystem.ThumbstickInput(ThumbSticks.LeftY, playerNr - 1));
        }

        face.localPosition = Vector2.Lerp(face.localPosition, direction * maxDistance, lookSpeed);

        #endregion
    }
}
