using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer_Ability : MonoBehaviour, ISpecialAbility
{
    //public float cooldown;
    public GameObject minePrefab;
    public float timeout;

    private PlayerController controller;
    private Explosive_ContactDamage placedMine;
    private int mineLayer;
    private bool pressed = false;
    private bool placed = false;
    private bool canPlace = true;

    public string toxicMineSound = "toxicMine";


    void Start()
    {
        mineLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (!pressed)
        {
            pressed = true;

            if (placed && placedMine == null)
            {
                placed = false;
                StartCoroutine(Cooldown());
            }

            if (!placed && canPlace)
            {
                AudioManager1.instance.Play(toxicMineSound);
                placedMine = Instantiate(minePrefab, transform.position, Quaternion.identity).GetComponent<Explosive_ContactDamage>();
                placedMine.gameObject.layer = mineLayer;
                placedMine.transform.Find("Outline").GetComponent<SpriteRenderer>().color = controller.playerColor;
                //StartCoroutine(Cooldown());
                placed = true;
                canPlace = false;
            }
            else if (placed)
            {
                placedMine.Explode();
                placed = false;
                StartCoroutine(Cooldown());
            }
        }
    }

    public void StopUse()
    {
        pressed = false;
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(timeout);
        canPlace = true;
    }
}
