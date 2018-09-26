using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int playerNr = 1;
    public GameObject baseWeapon;
    public GameObject bonusWeapon;

    private GameObject currentItem;
    private int weaponLayer;
    private PlayerHealth health;
    private PlayerMovement movement;

    private bool baseActive = true;
    private bool gotBonus = false;


    void Awake()
    {
        health = GetComponent<PlayerHealth>();
        movement = GetComponent<PlayerMovement>();
    }

    void Start()
    {
        weaponLayer = LayerMask.NameToLayer("WeaponP" + playerNr);

        if (baseWeapon)
        {
            SetLayer(baseWeapon.transform, weaponLayer);
        }

        if (bonusWeapon != null)
        {
            gotBonus = true;
            SetLayer(bonusWeapon.transform, weaponLayer);
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
        SetLayer(currentItem.transform, gameObject.layer);
    }

    public void WeaponPickUp(GameObject newWeapon)
    {
        if (gotBonus)
            Destroy(bonusWeapon);

        bonusWeapon = newWeapon;
        SetLayer(bonusWeapon.transform, weaponLayer);
        gotBonus = true;
        baseActive = false;
        baseWeapon.SetActive(false);
        bonusWeapon.SetActive(true);
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public IEnumerator Stun(float timeout)
    {
        health.stunned = true;
        movement.enabled = false;

        yield return new WaitForSeconds(timeout);

        health.stunned = false;

        if (!health.frozen)
            movement.enabled = true;
    }

    public IEnumerator Thaw(float timeout)
    {
        health.frozen = true;
        movement.enabled = false;

        yield return new WaitForSeconds(timeout);

        health.frozen = false;
        movement.enabled = true;
    }

    public void InstantThaw()
    {
        health.frozen = false;
        movement.enabled = true;
    }

    public void StartPlayerProtection()
    {
        StopAllCoroutines();
        health.enabled = false;
    }

    public void StopPlayerProtection(float ionPassOnDmg)
    {
        health.enabled = true;
        if (ionPassOnDmg > 0)
            health.IonDamage(ionPassOnDmg);
    }
}
