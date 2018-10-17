using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IPlanet
{
    public float range;
    public LayerMask targetMask;
    public float hitLength;
    public WeaponController currentWeapon;
    public float searchRate = 1;
    private int weaponLayer;
    private Planet_HealthController health;
    private PlanetMovement movement;

    private Transform target;
    private Vector2 aimDir;

    private bool isFirePressed = false;
    private bool inputSet = false;
    private bool searching = true;
    private bool sprintActive = false;


    void Awake()
    {
        health = GetComponentInChildren<Planet_HealthController>();
        movement = GetComponent<PlanetMovement>();
    }

    void Start()
    {
        weaponLayer = LayerMask.NameToLayer("WeaponEnemy");

        if (currentWeapon)
        {
            SetLayer(currentWeapon.transform, weaponLayer);
        }

        StartCoroutine(FindTarget());
    }

    void Update()
    {
        ////Check for a Sprint
        //if (!sprintActive && Input.GetAxisRaw("Sprint") == 1)
        //{
        //    sprintActive = true;
        //    movement.isSprinting = true;
        //    currentWeapon.canAttack = false;
        //}
        //else if (sprintActive && Input.GetAxisRaw("Sprint") == 0)
        //{
        //    sprintActive = false;
        //    movement.isSprinting = false;
        //    currentWeapon.canAttack = true;
        //}

        if (target != null)
        {
            aimDir = (target.position - transform.position).normalized;
        }
        else if (!searching)
        {
            searching = true;
            StartCoroutine(FindTarget());
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

        currentWeapon.Aim(aimDir);
        currentWeapon.Shoot(isFirePressed);
    }

    private void FixedUpdate()
    {
        if (target != null && (target.position - transform.position).magnitude > range)
        {
            movement.direction = aimDir;
        }
        else
        {
            movement.direction = Vector2.zero;
        }
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public void Stun(bool stunActive)
    {
        movement.enabled = !stunActive;

        if (stunActive && sprintActive)
            return;

        currentWeapon.enabled = !stunActive;
    }

    public void SetPlanetProtection(bool isActive, float ionPassOnDmg = 0)
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

        if (target == null)
            StartCoroutine(FindTarget());
        else
            searching = false;
    }
}