using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer_Ability : MonoBehaviour, ISpecialAbility
{
    public float cooldown;
    public GameObject minePrefab;
    public ParticleSystem specialParticles;

    private PlayerController controller;
    private int mineLayer;
    private bool canAttack = true;
    private bool pressed = false;

    public string toxicMineSound = "toxicMine";


    void Start()
    {
        mineLayer = LayerMask.NameToLayer("Weapon" + LayerMask.LayerToName(gameObject.layer));
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (canAttack && !pressed)
        {
            AudioManager1.instance.Play(toxicMineSound);
            pressed = true;
            canAttack = false;
            specialParticles.Stop();

            GameObject mine = Instantiate(minePrefab, transform.position, Quaternion.identity);
            mine.layer = mineLayer;
            mine.transform.Find("Outline").GetComponent<SpriteRenderer>().color = controller.playerColor;
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
        specialParticles.Play();
        canAttack = true;
    }
}
