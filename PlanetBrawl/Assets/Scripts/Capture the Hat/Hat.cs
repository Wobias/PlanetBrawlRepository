using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    public GameObject player;
    private Transform hatTransform;
    private Vector3 hatOnPlayerPosition;
    private Vector3 newHatPosition;
    private bool isOnPlayer = false;

    private LayerMask weaponLayer;
    private int layerInt;

    // Use this for initialization
    void Start()
    {
        hatTransform = GetComponent<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckWeaponLayer(collision.gameObject.layer);

        if (isOnPlayer == false && collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            hatOnPlayerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 1.5f, 0f);
            hatTransform.position = hatOnPlayerPosition;
            gameObject.transform.parent = player.transform;
            gameObject.layer = player.layer;
            isOnPlayer = true;
        }
        else if (isOnPlayer == true && player.layer != layerInt && player.layer != collision.gameObject.layer)
        {
            newHatPosition = new Vector3(Random.Range(-20, 20), Random.Range(-9, 9), 0);
            gameObject.transform.parent = null;
            hatTransform.position = Vector3.Lerp(hatTransform.position, newHatPosition, 0.5f);
            gameObject.layer = 0;
            StartCoroutine(SwitchBoolAfterSeconds());
        }
    }

    void CheckWeaponLayer(LayerMask layer)
    {
        if (layer == 14)
        {
            layerInt = 8;
        }
        if (layer == 15)
        {
            layerInt = 11;
        }
        if (layer == 16)
        {
            layerInt = 12;
        }
        if (layer == 17)
        {
            layerInt = 13;
        }

    }

    IEnumerator SwitchBoolAfterSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        isOnPlayer = false;
        yield return null;
    }
}
