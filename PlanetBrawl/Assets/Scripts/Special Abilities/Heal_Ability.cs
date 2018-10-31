using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Ability : MonoBehaviour, ISpecialAbility
{
    public float healthPerSec = 10;


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
            controller.AbilityStun(true);
            StartCoroutine(HealOverTime());
            healing = true;
        }
    }

    public void StopUse()
    {
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
