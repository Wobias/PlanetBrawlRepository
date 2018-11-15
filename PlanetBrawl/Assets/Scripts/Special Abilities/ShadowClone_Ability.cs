using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone_Ability : MonoBehaviour, ISpecialAbility
{
    public GameObject clonePrefab;
    public float maxDistance;
    public float minDistance;
    //public float splitForce;

    private bool isSplit = false;
    private PlayerController controller;


    void Start()
    {
        controller = GetComponent<PlayerController>();
    }

    public void Use()
    {
        if (!isSplit)
        {
            isSplit = true;

            //int offsetMultiplier = Random.Range(0, 2);

            //if (offsetMultiplier == 0)
                //offsetMultiplier = -1;

            //Vector3 offset = new Vector2(Random.Range(minDistance, maxDistance) * offsetMultiplier, Random.Range(minDistance, maxDistance) * offsetMultiplier);
            GameObject newClone = Instantiate(clonePrefab, transform.position, Quaternion.identity, transform);
            SetLayer(newClone.transform, gameObject.layer);
            newClone.GetComponent<PlayerController>().playerNr = controller.playerNr;
            newClone.GetComponent<PlayerController>().playerColor = controller.playerColor;
        }
    }

    public void StopUse()
    {
        
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }
}
