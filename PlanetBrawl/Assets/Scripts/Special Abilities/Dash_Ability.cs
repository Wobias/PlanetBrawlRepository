using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash_Ability : MonoBehaviour, ISpecialAbility
{
    public float speed;
    public float duration;
    public ParticleSystem specialParticles;

    PlanetMovement playerMovement;
    PlayerController controller;
    bool canDash = true;


    void Start()
    {
        playerMovement = GetComponent<PlanetMovement>();
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (canDash && playerMovement.direction != Vector2.zero)
        {
            playerMovement.ApplyTempExForce(playerMovement.direction.normalized * speed, duration);
            controller.AbilityStun(true);
            canDash = false;
            specialParticles.Stop();
        }
        else if (!canDash)
        {
            controller.AbilityStun(false);
        }
    }

    public void StopUse()
    {
        controller.AbilityStun(false);
        canDash = true;
    }

}
