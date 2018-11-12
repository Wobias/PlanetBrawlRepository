using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController_Moon : WeaponController
{
    //VARIABLES
    #region

    public float punchSpeed = 1;
    public float retractSpeed = 1; //Should (usually) be slower than punch speed
    public float maxDistance = 5; //Maximum Shot Reach
    private OrbitState moonState = OrbitState.orbit; //The actual variable for that
    private bool fireHeld = false; //Helper Variable because Triggers are an Axis not a button

    private float startMaxDist;

    #endregion

    protected override void Start()
    {
        startMaxDist = maxDistance;
        base.Start();
    }

    public override bool Shoot (bool isFirePressed)
    {
        if (isFirePressed && canAttack && moonState == OrbitState.orbit && !fireHeld)
        {
            fireHeld = true;
            moonState = OrbitState.shooting;
            for (int i = 0; i < weaponParts.Length; i++)
            {
                weaponParts[i].isKinematic = false; //Unlock the moons position
                weaponColliders[i].enabled = true;
            }
        }
        //Check for Trigger Release
        //else if (!isFirePressed && moonState == OrbitState.shooting)
        //{
        //    //In case the moon is mid-punch, retract it
        //    moonState = OrbitState.retracting;
        //}

        if (!isFirePressed && fireHeld)
        {
            fireHeld = false;
        }

        for (int i = 0; i < weaponParts.Length; i++)
        {
            ShootFragment(weaponParts[i], weaponColliders[i], weaponMinPos[i]);
        }

        if (moonState == OrbitState.orbit)
            return true;
        else
            return false;
    }

    private void ShootFragment(Rigidbody2D frag, Collider2D fragCollider, Vector2 startPos)
    {
        switch (moonState)
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
                        moonState = OrbitState.retracting;
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
                        moonState = OrbitState.orbit;
                        frag.isKinematic = true;
                        fragCollider.enabled = false;
                    }
                    break;
                }
        }
    }

    public override void OnHit()
    {
        if (moonState == OrbitState.shooting)
        {
            moonState = OrbitState.retracting;
        }
    }

    public override void Aim(Vector2 direction)
    {
        if (moonState == OrbitState.orbit)
            base.Aim(direction);
    }

    protected override void OnDistanceReset()
    {
        maxDistance = startMaxDist * charTrans.localScale.x;
    }
}
