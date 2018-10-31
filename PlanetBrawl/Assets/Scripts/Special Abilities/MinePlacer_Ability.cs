using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer_Ability : MonoBehaviour, ISpecialAbility
{
    public float cooldown;
    public GameObject minePrefab;

    private int mineLayer;
    private bool canAttack = true;
    private bool pressed = false;


    void Start()
    {
        mineLayer = LayerMask.NameToLayer("WeaponPlayer" + GetComponent<PlayerController>().playerNr);
    }

    public void Use()
    {
        if (canAttack && !pressed)
        {
            pressed = true;
            canAttack = false;

            GameObject mine = Instantiate(minePrefab, transform.position, Quaternion.identity);
            mine.layer = mineLayer;
            StartCoroutine(Cooldown());
        }
    }

    public void StopUse()
    {
        pressed = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canAttack = true;
    }
}
