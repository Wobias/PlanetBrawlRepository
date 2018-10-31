using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlanet
{
    public int playerNr = 1;
    public Color playerColor;
    public WeaponController currentWeapon;
    public WeaponController backupWeapon;

    private GameObject currentItem;
    private int weaponLayer;
    private Planet_HealthController health;
    private PlanetMovement movement;
    private WeaponController weaponCacher;
    private ISpecialAbility ability;

    private bool isBonus = false;
    private bool isFirePressed;
    private bool canSwitch = true;
    private bool sprintActive = false;
    private bool specialAvailable = true;


    void Awake()
    {
        health = GetComponentInChildren<Planet_HealthController>();
        movement = GetComponent<PlanetMovement>();
        ability = GetComponent<ISpecialAbility>();
    }

    void Start()
    {
        weaponLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));

        if (currentWeapon)
        {
            SetLayer(currentWeapon.transform, weaponLayer);
        }

        if (backupWeapon != null)
        {
            SetLayer(backupWeapon.transform, weaponLayer);
        }
    }

    void Update()
    {
        if (currentWeapon == null && isBonus)
        {
            canSwitch = true;
            currentWeapon = backupWeapon;
            backupWeapon = null;
            currentWeapon.gameObject.SetActive(true);
            isBonus = false;
            
            if (sprintActive)
            {
                currentWeapon.canAttack = false;
            }
            else if (!health.stunned && !health.frozen)
            {
                currentWeapon.canAttack = true;
            }
        }

        //Check for a Sprint
        if (!sprintActive && Input.GetAxisRaw("Sprint" + playerNr) == 1)
        {
            sprintActive = true;
            movement.isSprinting = true;
            currentWeapon.canAttack = false;
        }
        else if (sprintActive && Input.GetAxisRaw("Sprint" + playerNr) == 0)
        {
            sprintActive = false;
            movement.isSprinting = false;
            currentWeapon.canAttack = true;
        }

        Vector2 aimDir = new Vector2(Input.GetAxis("AimHor" + playerNr), Input.GetAxis("AimVer" + playerNr));

        isFirePressed = Input.GetAxisRaw("Fire" + playerNr) == 1 ? true : false;

        currentWeapon.Aim(aimDir);
        canSwitch = currentWeapon.Shoot(isFirePressed);

        if (Input.GetButtonDown("SwitchWeapon" + playerNr) && !sprintActive)
        {
            SwapWeapons();
        }

        if (Input.GetButtonUp("Special" + playerNr) && ability != null)
        {
            ability.StopUse();
        }
        else if (Input.GetButton("Special" + playerNr) && specialAvailable && ability != null)
        {
            ability.Use();
        }
    }

    private void FixedUpdate()
    {
        movement.direction = new Vector2(Input.GetAxis("Horizontal" + playerNr), Input.GetAxis("Vertical" + playerNr));
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

    public void WeaponPickUp(WeaponController newWeapon)
    {
        if (!isBonus)
        {
            if (backupWeapon != null)
            {
                Destroy(backupWeapon.gameObject);
            }
            backupWeapon = newWeapon;
            SetLayer(backupWeapon.transform, weaponLayer);
            if (canSwitch)
                SwapWeapons();
            else
                backupWeapon.gameObject.SetActive(false);
        }
        else if (isBonus)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = newWeapon;
            SetLayer(currentWeapon.transform, weaponLayer);
            currentWeapon.gameObject.SetActive(true);
        }

        if (sprintActive)
        {
            currentWeapon.canAttack = false;
        }
    }

    void SwapWeapons()
    {
        if (!isBonus && backupWeapon == null || !canSwitch)
            return;

        weaponCacher = currentWeapon;
        currentWeapon = backupWeapon;
        backupWeapon = weaponCacher;
        backupWeapon?.gameObject.SetActive(false);
        currentWeapon?.gameObject.SetActive(true);
        isBonus = !isBonus;
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public void Stun(bool stunActive)
    {
        if (ability != null)
            ability.StopUse();

        specialAvailable = !stunActive;

        movement.enabled = !stunActive;

        if (sprintActive)
            stunActive = true;

        currentWeapon.canAttack = !stunActive;
    }

    public void AbilityStun(bool stunActive)
    {
        movement.enabled = !stunActive;

        if (sprintActive)
            stunActive = true;

        currentWeapon.canAttack = !stunActive;
    }

    public void SetPlanetProtection(bool isActive, float ionPassOnDmg=0)
    {
        if (isActive)
        {
            health.StopAllCoroutines();
            health.stunned = false;
            Stun(false);
            health.frozen = false;
            health.enabled = false;
        }
        else
        {
            health.enabled = true;
            if (ionPassOnDmg > 0)
                health.IonDamage(ionPassOnDmg);
        }
    }

    public void SetWeaponDistance()
    {
        currentWeapon.ResetMinPos();
    }
}
