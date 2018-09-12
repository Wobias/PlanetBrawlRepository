using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_Moon : MonoBehaviour
{
    public float punchSpeed = 1;
    public float retractSpeed = 1;
    public float rotationSpeed = 0.1f;
    public float minDistance = 1;
    public float maxDistance = 5;

    private enum MoonState { orbit, shooting, retracting };
    private MoonState moonState = MoonState.orbit;
    private bool triggerPressed = false;

    private Vector2 direction;
    private Quaternion targetRotation;
    private Transform moonPos;
    private Transform holderPos;
    private Rigidbody2D rb2d;


    void Start()
    {
        moonPos = transform;
        holderPos = moonPos.parent;
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetAxis("AimHor") != 0 || Input.GetAxis("AimVer") != 0)
        {
            direction = new Vector2(Input.GetAxis("AimHor"), Input.GetAxis("AimVer"));

            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //Apply the angle to the gun's rotation
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        holderPos.rotation = Quaternion.Lerp(holderPos.rotation, targetRotation, rotationSpeed);

        if (!triggerPressed && Input.GetAxisRaw("Fire1") == -1 && moonState == MoonState.orbit)
        {
            triggerPressed = true;
            moonState = MoonState.shooting;
            rb2d.isKinematic = false;
        }

        if (triggerPressed && Input.GetAxisRaw("Fire1") == 0)
        {
            triggerPressed = false;

            if (moonState == MoonState.shooting)
            {
                moonState = MoonState.retracting;
            }
        }

        if (moonState == MoonState.shooting)
        {
            if ((holderPos.position - moonPos.position).magnitude < maxDistance)
            {
                rb2d.velocity = -(holderPos.position - moonPos.position).normalized * retractSpeed;
            }
            else
            {
                moonState = MoonState.retracting;
            }
        }
        else if (moonState == MoonState.retracting)
        {
            if ((holderPos.position - moonPos.position).magnitude > minDistance)
            {
                rb2d.velocity = (holderPos.position - moonPos.position).normalized * retractSpeed;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
                moonPos.position = holderPos.position + minDistance * moonPos.right;
                moonState = MoonState.orbit;
                rb2d.isKinematic = true;
            }
        }
    }
}
