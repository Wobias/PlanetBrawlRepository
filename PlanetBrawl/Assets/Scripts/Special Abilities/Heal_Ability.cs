using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Ability : MonoBehaviour, ISpecialAbility
{
    public float healthBonus = 2;
    public ParticleSystem specialParticles;
    public ParticleSystem healParticles;


    private bool healed = false;
    private Planet_HealthController healthController;
    private PlayerController controller;


    void Start()
    {
        healthController = GetComponent<Planet_HealthController>();
        //controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (!healed)
        {
            specialParticles.Stop();
            healParticles.Play();
            //controller.AbilityStun(true);
            //StartCoroutine(HealOverTime());
            healed = true;
            healthController.Heal(healthBonus);
        }
    }

    public void StopUse()
    {
        //healParticles.Stop();
        //specialParticles.Play();
        //healed = false;
        //controller.AbilityStun(false);
    }

    //IEnumerator HealOverTime()
    //{
    //    yield return new WaitForSeconds(1);

    //    if (healed)
    //    {
    //        healthController.Heal(healthBonus);
    //        healed = false;
    //    }
    //}
}
