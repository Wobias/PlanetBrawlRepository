using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Ability : MonoBehaviour, ISpecialAbility
{
    public float healthPerSec = 10;
    public ParticleSystem specialParticles;
    public ParticleSystem healParticles;


    private bool healing = false;
    private Planet_HealthController healthController;
    private PlayerController controller;


    void Start()
    {
        healthController = GetComponent<Planet_HealthController>();
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (!healing)
        {
            specialParticles.Stop();
            healParticles.Play();
            controller.AbilityStun(true);
            StartCoroutine(HealOverTime());
            healing = true;
        }
    }

    public void StopUse()
    {
        healParticles.Stop();
        specialParticles.Play();
        healing = false;
        controller.AbilityStun(false);
    }

    IEnumerator HealOverTime()
    {
        yield return new WaitForSeconds(1);

        if (healing)
        {
            healthController.Heal(healthPerSec);
            healing = false;
        }
    }
}
