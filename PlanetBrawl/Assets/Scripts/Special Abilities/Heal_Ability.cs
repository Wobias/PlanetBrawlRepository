using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Ability : MonoBehaviour, ISpecialAbility
{
    public float healthBonus = 3;
    public float healTimeout;
    public ParticleSystem healParticles;

    private bool canHeal = true;
    private bool isPressed = false;
    private Player_HealthController healthController;
    //private PlayerController controller;
    private int playerNr;
    private PlayerUI playerUI;

    void Start()
    {

        healthController = GetComponent<Player_HealthController>();
        playerNr = GetComponent<PlayerController>().playerNr;
        playerUI = FindObjectOfType<PlayerUI>();
    }

    public void Use()
    {
        if (canHeal && !isPressed)
        {
            healParticles.Play();
            //controller.AbilityStun(true);
            //StartCoroutine(HealOverTime());
            canHeal = false;
            healthController.Heal(healthBonus);
            playerUI?.AbilityCooldown(healTimeout, playerNr);
        }

        isPressed = true;
    }

    public void StopUse()
    {
        isPressed = false;
        //healParticles.Stop();
        //specialParticles.Play();
        //healed = false;
        //controller.AbilityStun(false);
    }

    public void ResetAbility()
    {
        canHeal = true;
    }
}
