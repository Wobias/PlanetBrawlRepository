using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone_Ability : MonoBehaviour, ISpecialAbility
{
    public GameObject clonePrefab;
    public float maxDistance;
    public float minDistance;
    public float timeout;

    private bool isSplit = false;
    private bool pressed = false;
    private bool canClone = true;
    private PlayerController controller;
    private PlayerUI playerUI;


    void Start()
    {
        controller = GetComponent<PlayerController>();
        playerUI = FindObjectOfType<PlayerUI>();
    }

    public void Use()
    {
        if (!pressed && canClone)
        {
            pressed = true;
            canClone = false;
            GameObject newClone = Instantiate(clonePrefab, transform.position, Quaternion.identity, transform);
            GameManager.SetLayer(newClone.transform, gameObject.layer);
            newClone.GetComponent<PlayerController>().playerNr = controller.playerNr;
            newClone.GetComponent<PlayerController>().playerColor = controller.playerColor;

            playerUI?.AbilityCooldown(timeout, controller.playerNr);
        }
    }

    public void StopUse()
    {
        pressed = false; 
    }

    public void ResetAbility()
    {
        canClone = true;
    }
}
