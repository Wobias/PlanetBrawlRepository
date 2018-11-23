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


    void Start()
    {
        healthController = GetComponent<Player_HealthController>();
        //controller = GetComponent<PlayerController>();
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
            StartCoroutine(ResetHeal());
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

    IEnumerator ResetHeal()
    {
        yield return new WaitForSeconds(healTimeout);

        canHeal = true;
    }
}
