using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController_AsteroidBelt : WeaponController
{
    //VARIABLES
    #region

    public float punchSpeed = 1;
    public float retractSpeed = 1; //Should (usually) be slower than punch speed
    public float maxDistance = 5; //Maximum Shot Reach
    public bool spinRight = true;

    private float startMaxDist;
    private OrbitState asteroidState = OrbitState.orbit; //The actual variable for that
    private bool fireHeld = false; //Helper Variable because Triggers are an Axis not a button
    private int spinDir = 1;

    #endregion

    protected override void Start()
    {
        startMaxDist = maxDistance;

        base.Start();

        if (spinRight)
        {
            spinDir = -1;
        }
    }

    public override void Aim(Vector2 direction)
    {
        origin.Rotate(new Vector3(0, 0, rotationSpeed * spinDir * Time.deltaTime));
    }

    public override bool Shoot(bool isFirePressed)
    {
        if (isFirePressed && !fireHeld && canAttack && asteroidState == OrbitState.orbit)
        {
            fireHeld = true;
            asteroidState = OrbitState.shooting;
            for (int i = 0; i < weaponParts.Length; i++)
            {
                weaponParts[i].isKinematic = false; //Unlock the moons position
                weaponColliders[i].enabled = true;
            }
        }
        //Check for Trigger Release
        else if (!isFirePressed && asteroidState == OrbitState.shooting)
        {
            //In case the moon is mid-punch, retract it
            asteroidState = OrbitState.retracting;
        }

        if (!isFirePressed && fireHeld)
        {
            fireHeld = false;
        }

        for (int i = 0; i < weaponParts.Length; i++)
        {
            ShootFragment(weaponParts[i], weaponColliders[i], weaponMinPos[i]);
        }

        if (asteroidState == OrbitState.orbit)
            return true;
        else
            return false;
    }

    private void ShootFragment(Rigidbody2D frag, Collider2D fragCollider, Vector2 startPos)
    {
        switch (asteroidState)
        {
            case OrbitState.orbit:
                {
                    if (!frag.isKinematic)
                    {
                        frag.velocity = Vector2.zero;
                        frag.transform.position = origin.position + startPos.y * frag.transform.up + startPos.x * frag.transform.right; //Reset the position in case it overshot the minDistance
                        frag.isKinematic = true;
                        fragCollider.enabled = false;
                    }
                    break;
                }
            case OrbitState.shooting:
                {
                    if ((origin.position - frag.transform.position).magnitude < maxDistance)
                    {
                        //If the moon is below maxDistance it gets shot further
                        frag.velocity = -(origin.position - frag.transform.position).normalized * punchSpeed;
                    }
                    else
                    {
                        //If the moon has reached maxDistance it starts retracting
                        asteroidState = OrbitState.retracting;
                    }
                    break;
                }
            case OrbitState.retracting:
                {
                    if ((origin.position - frag.transform.position).magnitude > Mathf.Abs(startPos.magnitude))
                    {
                        //If the moon is above minDistance it keeps retracting
                        frag.velocity = (origin.position - frag.transform.position).normalized * retractSpeed;
                    }
                    else
                    {
                        //If the moon has reached minDistance stop it and set it to kinematic
                        frag.velocity = Vector2.zero;
                        frag.transform.position = origin.position + startPos.y * frag.transform.up + startPos.x * frag.transform.right; //Reset the position in case it overshot the minDistance
                        asteroidState = OrbitState.orbit;
                        frag.isKinematic = true;
                        fragCollider.enabled = false;
                    }
                    break;
                }
        }
    }

    protected override void OnDistanceReset()
    {
        maxDistance = startMaxDist * charTrans.localScale.x;
    }
}
