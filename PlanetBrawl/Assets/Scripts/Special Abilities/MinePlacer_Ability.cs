using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer_Ability : MonoBehaviour, ISpecialAbility
{
    //public float cooldown;
    public GameObject minePrefab;
    public ParticleSystem specialParticles;

    private PlayerController controller;
    private GameObject placedMine;
    private int mineLayer;
    private bool pressed = false;
    private bool placed = false;


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
            //specialParticles.Stop();

            if (!placed)
            {
                placedMine = Instantiate(minePrefab, transform.position, Quaternion.identity);
                placedMine.layer = mineLayer;
                placedMine.transform.Find("Outline").GetComponent<SpriteRenderer>().color = controller.playerColor;
                //StartCoroutine(Cooldown());
                placed = true;
            }
            else
            {
                Destroy(placedMine);
                placed = false;
            }
        }
    }

    public void StopUse()
    {
        pressed = false;
    }

    //IEnumerator Cooldown()
    //{
    //    yield return new WaitForSeconds(cooldown);
    //    specialParticles.Play();
    //    canAttack = true;
    //}
}
