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
    private PlayerUI playerUI;


    void Start()
    {
        playerMovement = GetComponent<PlanetMovement>();
        controller = GetComponent<PlayerController>();
        playerUI = FindObjectOfType<PlayerUI>();
    }

    public void Use()
    {
        if (canDash && !isPressed && playerMovement.direction != Vector2.zero)
        {
            playerMovement.ApplyTempExForce(playerMovement.direction.normalized * speed, duration);
            controller.AbilityStun(true);
            canDash = false;
            playerUI?.AbilityCooldown(timeout, controller.playerNr);
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

    public void ResetAbility()
    {
        canDash = true;
    }
}
