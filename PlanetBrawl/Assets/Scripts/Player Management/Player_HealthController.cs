using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthState {full, average, low, critical};

public class Player_HealthController : HealthController
{
    //Health Variables
    #region
    public GameObject dropPrefab;

    public GameObject destroyPlanet;

    private GameObject planetChild;
    private GameObject weaponOrbit;

    protected float hpPercent = 100f;
    protected IPlanet controller;
    protected Animator animator;

    private Hat hat;

    public static HealthState healthState = HealthState.full;

    Transform myTransform;
    Vector3 spawnPoint;

    protected PlanetMovement planetMovement;

    private PlayerUI playerUI;
    private int playerNumber;

    #endregion


    protected override void Start()
    {
        base.Start();
        playerUI = FindObjectOfType<PlayerUI>();

        if (GetComponent<PlayerController>() != null)
        {
            playerNumber = GetComponent<PlayerController>().playerNr;
        }
        

        animator = GetComponent<Animator>();
        controller = GetComponent<IPlanet>();
        planetMovement = GetComponent<PlanetMovement>();
        myTransform = GetComponentInChildren<SpriteRenderer>().transform;
        spawnPoint = transform.position;
        hat = FindObjectOfType<Hat>();
        planetChild = transform.GetChild(0).gameObject;
        weaponOrbit = transform.GetChild(1).gameObject;
    }

    protected override void OnHealthChange(bool damage=true)
    {
        hpPercent = (health * 100f) / maxHealth;

        if (animator != null)
        {
            animator.SetFloat("HealthPercent", hpPercent);
            if (damage)
            {
                animator.SetTrigger("Hit");
            }
            else
            {
                animator.SetTrigger("Heal");
            }
        }
    }

    protected override void StunObject(bool stunActive)
    {
        if (controller != null)
            controller.Stun(stunActive);
    }

    protected override void Kill()
    {
        if (attackerNr != 0 && GameManager.instance.gameMode == GameModes.deathmatch ||
            GameManager.instance.gameMode == GameModes.teamdeathmatch)
        {
            GameManager.instance.AddScore(attackerNr);
        }
        else if (GameManager.instance.gameMode == GameModes.survival)
        {
            GameManager.instance.AddScore(GetComponent<PlayerController>().playerNr);
            Destroy(gameObject);
        }

        if (dropPrefab != null)
        {
            GameObject drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);

            if (drop.GetComponent<Weapon_ContactDamage>())
            {
                drop.layer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));
                drop.transform.Find("Outline").GetComponent<SpriteRenderer>().color = GetComponent<PlayerController>().playerColor;
            }
        }

        destroyPlanet.transform.parent = null;

        destroyPlanet.SetActive(true);

        planetChild.SetActive(false);
        weaponOrbit.SetActive(false);

        StartCoroutine("ResetHealthAndPosition");
        
    }

    public override void Hit(float physicalDmg, DamageType dmgType, Vector2 knockbackForce, float stunTime, int attackNr = 0, float effectTime = 0)
    {
        base.Hit(physicalDmg, dmgType, knockbackForce, stunTime, attackNr, effectTime);
        
        if (hat != null && hat.transform.parent == transform)
        {
            hat.ThrowHat();
        }
    }

    public virtual void Heal(float bonusHealth)
    {
        health += bonusHealth;
        if (health > maxHealth)
            health = maxHealth;

        OnHealthChange(false);
    }

    protected override void SetPoison(bool active)
    {
        base.SetPoison(active);
        planetMovement.invertedMove = active;
        controller.InvertAim(active);
    }

    protected override void SetFire(bool active)
    {
        base.SetFire(active);
        planetMovement.SetBrakes(!active);
    }

    private IEnumerator ResetHealthAndPosition()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        attackerNr = 0;
        health = maxHealth;
        transform.position = spawnPoint;
        playerUI.ActivateHealthBars(playerNumber - 1);
        destroyPlanet.transform.position = transform.position;
        destroyPlanet.transform.parent = transform;

        planetChild.SetActive(true);
        weaponOrbit.SetActive(true);
        yield return null;
    }
}
