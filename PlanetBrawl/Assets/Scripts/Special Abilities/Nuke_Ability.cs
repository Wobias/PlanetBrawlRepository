using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nuke_Ability : MonoBehaviour, ISpecialAbility
{
    public GameObject nukePrefab;
    public Transform origin;
    public float timeout;

    private GameObject currentNuke;
    private PlayerController controller;
    private int nukeLayer;
    private int playerNr;
    private bool canAttack = true;
    private bool inAction = false;
    private bool pressed = false;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        nukeLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));
        playerNr = GetComponent<PlayerController>().playerNr;
    }

    public void Use()
    {
        if (canAttack && !inAction && !pressed)
        {
            canAttack = false;
            inAction = true;
            controller.AbilityStun(true, true);

            currentNuke = Instantiate(nukePrefab, transform.position, origin.rotation);
            currentNuke.layer = nukeLayer;
            currentNuke.GetComponent<WeaponController_Nuke>().playerNr = playerNr;
            currentNuke.GetComponent<WeaponController_Nuke>().playerLayer = gameObject.layer;
            currentNuke.transform.Find("Outline").GetComponent<SpriteRenderer>().color = controller.playerColor;
        }
        else if (inAction && currentNuke == null)
        {
            inAction = false;
            controller.AbilityStun(false);
            StartCoroutine(ResetNuke());
        }
    }

    public void StopUse()
    {
        if (pressed)
            pressed = false;

        if (inAction)
        {
            inAction = false;
            controller.AbilityStun(false);

            currentNuke?.GetComponent<Explosive_ContactDamage>().Explode();

            StartCoroutine(ResetNuke());
        } 
    }

    IEnumerator ResetNuke()
    {
        yield return new WaitForSeconds(timeout);
        canAttack = true;
    }
}
