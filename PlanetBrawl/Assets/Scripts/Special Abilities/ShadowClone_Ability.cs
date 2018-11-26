using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone_Ability : MonoBehaviour, ISpecialAbility
{
    public GameObject clonePrefab;
    public float maxDistance;
    public float minDistance;
    public float timeout;

    private bool isSplit = false;
    private bool pressed = false;
    private bool canClone = true;
    private PlayerController controller;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (!pressed && canClone)
        {
            pressed = true;
            canClone = false;
            GameObject newClone = Instantiate(clonePrefab, transform.position, Quaternion.identity, transform);
            GameManager.SetLayer(newClone.transform, gameObject.layer);
            newClone.GetComponent<PlayerController>().playerNr = controller.playerNr;
            newClone.GetComponent<PlayerController>().playerColor = controller.playerColor;

            StartCoroutine(ResetClone());
        }
    }

    public void StopUse()
    {
        pressed = false; 
    }

    IEnumerator ResetClone()
    {
        yield return new WaitForSeconds(timeout);
        canClone = true;
    }
}
