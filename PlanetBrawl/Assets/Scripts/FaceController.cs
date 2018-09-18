using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceController : MonoBehaviour
{
    public int player = 1;
    public float maxDistance = 0.1f;
    public float lookSpeed = 0.25f;

    private Vector2 direction;
    private Transform face;


    void Start()
    {
        face = transform;
    }

    void Update ()
    {
        //AIMING
        #region

        //Get the Aiming Direction
        direction = new Vector2(Input.GetAxis("AimHor" + player), Input.GetAxis("AimVer" + player));

        if (direction == Vector2.zero)
        {
            direction = new Vector2(Input.GetAxis("Horizontal" + player), Input.GetAxis("Vertical" + player));
        }

        face.localPosition = Vector2.Lerp(face.localPosition, direction * maxDistance, lookSpeed);

        #endregion
    }
}
