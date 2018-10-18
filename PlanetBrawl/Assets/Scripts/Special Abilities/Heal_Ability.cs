using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Ability : MonoBehaviour, ISpecialAbitities
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
            StartCoroutine(HealOverTime());
            healing = true;
        }
    }

    IEnumerator HealOverTime()
    {
        yield return new WaitForSeconds(1);

        healthController.Heal(healthPerSec);
        healing = false;
    }
}
