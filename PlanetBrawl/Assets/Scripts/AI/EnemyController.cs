using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPlanet
{
    public float range;
    public LayerMask targetMask;
    public float hitLength;
    public WeaponController currentWeapon;
    public float searchRate = 2;
    public bool controlsInverted = false;


    private int weaponLayer;
    private Player_HealthController health;
    private PlanetMovement movement;

    private Transform target;
    private Vector2 aimDir;
    private bool isFirePressed = false;

    private bool inputSet = false;
    private bool searching = true;
    private bool stunned = false;


    void Awake()
    {
        health = GetComponentInChildren<Player_HealthController>();
        movement = GetComponent<PlanetMovement>();
    }

    void Start()
    {
        weaponLayer = LayerMask.NameToLayer("WeaponEnemy");

        if (currentWeapon)
        {
            GameManager.SetLayer(currentWeapon.transform, weaponLayer);
        }

        if (controlsInverted)
            movement.invertedMove = true;

        StartCoroutine(FindTarget());
    }

    void Update()
    {
        if (!stunned)
        {
            if (target != null)
            {
                aimDir = (target.position - transform.position).normalized;

                if (controlsInverted)
                    aimDir *= -1;
            }

            if (!inputSet)
            {
                isFirePressed = Physics2D.Raycast(transform.position, aimDir, range, targetMask);

                if (isFirePressed)
                {
                    inputSet = true;
                    StartCoroutine(SwitchTrigger());
                }
            }
        }

        currentWeapon.Aim(aimDir);
        currentWeapon.Shoot(isFirePressed);
    }

    private void FixedUpdate()
    {
        if (!movement.stunned && target != null && (target.position - transform.position).magnitude > range)
        {
            
            movement.direction = Vector2.Lerp(movement.direction, new Vector2(aimDir.x, aimDir.y), movement.inputRolloff);
        }
        else
        {
            movement.direction = Vector2.Lerp(movement.direction, Vector2.zero, movement.inputRolloff);
        }
    }

    public void Stun(bool stunActive)
    {
        stunned = stunActive;

        movement.stunned = stunActive;

        currentWeapon.canAttack = !stunActive;
    }

    public void SetPlanetProtection(bool isActive, float ionPassOnDmg = 0)
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
    }

    IEnumerator SwitchTrigger()
    {
        yield return new WaitForSeconds(hitLength);

        isFirePressed = false;

        yield return new WaitForSeconds(hitLength);

        inputSet = false;
    }

    IEnumerator FindTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float distance;

        if (players.Length > 0)
        {
            target = players[0].transform;
            distance = (target.position - transform.position).magnitude;

            for (int i = 0; i < players.Length; i++)
            {
                if ((players[i].transform.position - transform.position).magnitude < distance)
                {
                    target = players[i].transform;
                    distance = (target.position - transform.position).magnitude;
                }
            }
        }
      
        yield return new WaitForSeconds(searchRate);

        StartCoroutine(FindTarget());
    }

    public void InvertAim(bool active)
    {
        controlsInverted = active;
    }

    public void BoostWeapon(DamageType type, float effectTime)
    {
        return;
    }
}