using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke_Ability : MonoBehaviour, ISpecialAbility
{
    public GameObject nukePrefab;
    public Transform origin;

    private GameObject currentNuke;
    private PlayerController controller;
    private int nukeLayer;
    private int playerNr;
    private bool canAttack = true;
    private bool inAction = false;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        nukeLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));
        playerNr = GetComponent<PlayerController>().playerNr;
    }

    public void Use()
    {
        if (canAttack && !inAction)
        {
            canAttack = false;
            inAction = true;
            controller.AbilityStun(true, true);

            currentNuke = Instantiate(nukePrefab, transform.position, origin.rotation);
            currentNuke.layer = nukeLayer;
            currentNuke.GetComponent<WeaponController_Nuke>().playerNr = playerNr;
        }
        else if (inAction && currentNuke == null)
        {
            inAction = false;
            controller.AbilityStun(false);
        }
    }

    public void StopUse()
    {
        canAttack = true;
        controller.AbilityStun(false);

        if (currentNuke != null)
        {
            Destroy(currentNuke);
        }
    }
}
