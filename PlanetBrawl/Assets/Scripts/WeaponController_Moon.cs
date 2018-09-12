using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_Moon : MonoBehaviour
{
    public float punchSpeed = 1;
    public float retractSpeed = 1;
    public float minDistance = 1;
    public float maxDistance = 5;

    private bool canShoot = true;
    private enum MoonState { orbit, shooting, retracting };
    private MoonState moonState = MoonState.orbit;

    private Vector2 myVector2;
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
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            myVector2 = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            //Calculate an angle from the direction vector
            float angle = Mathf.Atan2(myVector2.y, myVector2.x) * Mathf.Rad2Deg;
            //Apply the angle to the gun's rotation
            holderPos.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

        if (Input.GetButtonDown("Fire1") && canShoot)
        {
            moonState = MoonState.shooting;
            canShoot = false;
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            moonState = MoonState.retracting;
        }

        if (moonState == MoonState.shooting)
        {
            if (moonPos.localPosition.x < maxDistance || moonPos.localPosition.y < maxDistance)
            {
                rb2d.velocity = moonPos.right * retractSpeed * Time.deltaTime;
            }
            else
            {
                moonState = MoonState.retracting;
            }
        }
        else if (moonState == MoonState.retracting)
        {
            if (moonPos.localPosition.x > minDistance || moonPos.localPosition.y > minDistance)
            {
                rb2d.velocity = -moonPos.right * retractSpeed * Time.deltaTime;
            }
            else
            {
                rb2d.velocity = Vector2.zero;
                moonPos.position = moonPos.right * minDistance;
                moonState = MoonState.orbit;
                canShoot = true;
            }
        }
    }
}
