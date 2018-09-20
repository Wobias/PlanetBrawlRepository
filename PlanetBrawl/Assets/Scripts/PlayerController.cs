using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNr = 1;
    public GameObject baseWeapon;
    public GameObject bonusWeapon;

    private GameObject currentItem;

    private bool baseActive = true;
    private bool gotBonus = false;


    void Start()
    {
        if (bonusWeapon != null)
        {
            gotBonus = true;
        }
    }

    void Update()
    {
        if (gotBonus && bonusWeapon == null)
        {
            baseActive = true;
            gotBonus = false;
            baseWeapon.SetActive(true);
        }

        if (Input.GetButtonDown("SwitchWeapon" + playerNr))
        {
            if (baseActive && gotBonus)
            {
                baseActive = false;
                baseWeapon.SetActive(false);
                bonusWeapon.SetActive(true);
            }
            else
            {
                baseActive = true;
                baseWeapon.SetActive(true);
                if (gotBonus)
                {
                    bonusWeapon.SetActive(false);
                }
            }
        }
    }

    public void ItemPickup(GameObject newItem)
    {
        if (currentItem != null)
        {
            Destroy(currentItem);
        }

        currentItem = newItem;
    }

    public void WeaponPickUp(GameObject newWeapon)
    {
        if (gotBonus)
            Destroy(bonusWeapon);

        bonusWeapon = newWeapon;
        gotBonus = true;
        baseActive = false;
        baseWeapon.SetActive(false);
        bonusWeapon.SetActive(true);
    }
}
