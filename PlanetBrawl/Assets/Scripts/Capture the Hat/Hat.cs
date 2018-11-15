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
    private bool isLerping = false;
    private float distance;
    

    // Use this for initialization
    void Start()
    {
        hatTransform = GetComponent<Transform>();
    }

    private void Update()
    {
        if (isLerping == true)
        {
            StartCoroutine(HatPositionLerp());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject rootObject = collision.transform.root.gameObject;

        if (isOnPlayer == false && rootObject.tag == "Player")
        {
            player = collision.gameObject;
            gameObject.transform.parent = player.transform;
            hatOnPlayerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, 0f);
            hatTransform.position = hatOnPlayerPosition;
            gameObject.layer = player.layer;
            isOnPlayer = true;
        }
        else if (isOnPlayer == true && player.layer != rootObject.layer)
        {
            gameObject.layer = 0;
            gameObject.transform.parent = null;
            newHatPosition = new Vector3(Random.Range(-20f, 20f), Random.Range(-9f, 9f), 0f);
            isLerping = true;
            StartCoroutine(SwitchBoolAfterSeconds());
        }
    }

    IEnumerator SwitchBoolAfterSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        isOnPlayer = false;
        yield return null;
    }

    IEnumerator HatPositionLerp()
    {
        hatTransform.position = Vector3.Lerp(hatTransform.position, newHatPosition, 10f * Time.deltaTime);
        yield return new WaitForSeconds(1f);
        isLerping = false;
        yield return null;
    }
}
