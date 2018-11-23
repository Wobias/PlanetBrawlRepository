using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Ability : MonoBehaviour, ISpecialAbility
{
    public float speed;
    public float duration;
    public float timeout;

    PlanetMovement playerMovement;
    PlayerController controller;
    bool canDash = true;
    private bool isPressed = false;


    void Start()
    {
        playerMovement = GetComponent<PlanetMovement>();
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (canDash && !isPressed && playerMovement.direction != Vector2.zero)
        {
            playerMovement.ApplyTempExForce(playerMovement.direction.normalized * speed, duration);
            controller.AbilityStun(true);
            canDash = false;
            StartCoroutine(ResetDash());
        }
        else if (!canDash)
        {
            controller.AbilityStun(false);
        }

        isPressed = true;
    }

    public void StopUse()
    {
        controller.AbilityStun(false);
        isPressed = false;
    }


    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(timeout);
        canDash = true;
    }
}
