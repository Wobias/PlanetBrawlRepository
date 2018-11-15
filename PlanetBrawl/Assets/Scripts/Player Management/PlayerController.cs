using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPlanet
{
    public int playerNr = 1;
    public Color playerColor;
    public WeaponController currentWeapon;
    public WeaponController bonusWeapon;
    public bool controlsInverted = false;


    private GameObject currentItem;
    private int weaponLayer;
    private Planet_HealthController health;
    private PlanetMovement movement;
    //private WeaponController weaponCacher;
    private ISpecialAbility ability;

    private bool bonusActive = false;
    private bool stunned = false;
    //private bool isFirePressed;
    //private bool canSwitch = true;
    //private bool sprintActive = false;
    private bool specialAvailable = true;


    void Awake()
    {
        health = GetComponentInChildren<Planet_HealthController>();
        movement = GetComponent<PlanetMovement>();
        ability = GetComponent<ISpecialAbility>();
        Camera.main.GetComponent<CameraFollow>().AddPlayers(gameObject.transform);
    }

    void Start()
    {
        weaponLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));

        if (currentWeapon)
        {
            SetLayer(currentWeapon.transform, weaponLayer);
        }

        if (controlsInverted)
            movement.invertedMove = true;
    }

    void Update()
    {
        if (currentWeapon == null && bonusActive)
        {
            bonusActive = false;
            currentWeapon = bonusWeapon;
            currentWeapon.gameObject.SetActive(true);
            bonusWeapon = null;
        }

        //    if (sprintActive)
        //    {
        //        currentWeapon.canAttack = false;
        //    }
        //    else if (!health.stunned && !health.frozen)
        //    {
        //        currentWeapon.canAttack = true;
        //    }
        //}

        //Check for a Sprint
        //if (!sprintActive && InputSystem.TriggerPressed(Trigger.Left, playerNr-1))
        //{
        //    sprintActive = true;
        //    movement.isSprinting = true;
        //    mainWeapon.canAttack = false;
        //    if (bonusWeapon)
        //        bonusWeapon.canAttack = false;
        //}
        //else if (sprintActive && InputSystem.TriggerUp(Trigger.Left, playerNr-1))
        //{
        //    sprintActive = false;
        //    movement.isSprinting = false;
        //    mainWeapon.canAttack = true;
        //    if (bonusWeapon)
        //        bonusWeapon.canAttack = true;
        //}

        if (!stunned)
        {
            Vector2 aimDir = new Vector2(InputSystem.ThumbstickInput(ThumbStick.RightX, playerNr - 1), InputSystem.ThumbstickInput(ThumbStick.RightY, playerNr - 1));

            if (controlsInverted)
                aimDir *= -1;

            currentWeapon.Aim(aimDir);
        }
        
        currentWeapon.Shoot(InputSystem.TriggerPressed(Trigger.Right, playerNr-1));

        if (ability != null)
        {
            if (InputSystem.TriggerUp(Trigger.Left, playerNr - 1))
            {
                ability.StopUse();
            }
            else if (InputSystem.TriggerPressed(Trigger.Left, playerNr - 1) && specialAvailable)// && !sprintActive)
            {
                ability.Use();
            }
        }
    }

    private void FixedUpdate()
    {
        if (!movement.stunned)
        {
            Vector2 dir = Vector2.Lerp(movement.direction, new Vector2(InputSystem.ThumbstickInput(ThumbStick.LeftX, playerNr - 1), InputSystem.ThumbstickInput(ThumbStick.LeftY, playerNr - 1)), movement.inputRolloff);
            movement.direction = dir;

            //movement.direction = new Vector2(InputSystem.ThumbstickInput(ThumbStick.LeftX, playerNr - 1), InputSystem.ThumbstickInput(ThumbStick.LeftY, playerNr - 1));
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

    public void WeaponPickUp(WeaponController newWeapon)
    {
        if (bonusActive)
        {
            Destroy(currentWeapon.gameObject);
            currentWeapon = newWeapon;
        }
        else
        {
            bonusWeapon = currentWeapon;
            currentWeapon = newWeapon;
        }

        bonusActive = true;
        SetLayer(currentWeapon.transform, weaponLayer);
        bonusWeapon.gameObject.SetActive(false);
        currentWeapon.gameObject.SetActive(true);
    }

    //void SwapWeapons()
    //{
    //    if (!isBonus && bonusWeapon == null || !canSwitch)
    //        return;

    //    weaponCacher = currentWeapon;
    //    currentWeapon = bonusWeapon;
    //    bonusWeapon = weaponCacher;
    //    bonusWeapon?.gameObject.SetActive(false);
    //    currentWeapon?.gameObject.SetActive(true);
    //    isBonus = !isBonus;
    //}

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public void Stun(bool stunActive)
    {
        if (stunActive)
        {
            InputSystem.Rumble(new Vector2(0.5f, 0.5f), 0.25f, playerNr-1);

            if (ability != null)
                ability.StopUse();
        }

        stunned = stunActive;

        specialAvailable = !stunActive;

        movement.stunned = stunActive;

        //if (sprintActive)
        //    stunActive = true;

        currentWeapon.canAttack = !stunActive;
        if (bonusWeapon)
            bonusWeapon.canAttack = !stunActive;
    }

    public void AbilityStun(bool stunActive, bool flushMovement=false)
    {
        stunned = stunActive;

        movement.stunned = stunActive;

        if (flushMovement && stunActive)
            movement.FlushVelocity();

        //if (sprintActive)
        //    stunActive = true;

        currentWeapon.canAttack = !stunActive;
        if (bonusWeapon)
            bonusWeapon.canAttack = !stunActive;
    }

    public void SetPlanetProtection(bool isActive, float ionPassOnDmg=0)
    {
        if (isActive)
        {
            health.StopAllCoroutines();
            health.stunned = false;
            Stun(false);
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
        if (bonusWeapon)
            bonusWeapon.ResetMinPos();
    }

    public void InvertAim(bool active)
    {
        controlsInverted = active;
    }

    public void BoostWeapon(DamageType type, float effectTime)
    {
        currentWeapon.ApplyElement(type, effectTime);
    }
}
