using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    private GameObject player;
    private Transform hatTransform;
    private Vector3 hatOnPlayerPosition;
    private bool isOnPlayer = false;
    private bool isLerping = false;
    private float distance;
    private Coroutine scoreRoutine;
    public Transform[] hatSpawns;

    int spawnIndex;


    // Use this for initialization
    void Start()
    {
        hatTransform = transform;
    }

    private void Update()
    {
        if (isLerping == true)
        {
            hatTransform.position = Vector3.Lerp(hatTransform.position, hatSpawns[spawnIndex].position, 10f * Time.deltaTime);
            if (hatTransform.position == hatSpawns[spawnIndex].position)
            {
                isLerping = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject rootObject = other.transform.root.gameObject;

        if (isOnPlayer == false && rootObject.tag == "Player")
        {
            player = rootObject;
            gameObject.transform.parent = player.transform;
            hatOnPlayerPosition = new Vector3(player.transform.position.x, player.transform.position.y + 0.8f, 0f);
            hatTransform.position = hatOnPlayerPosition;
            isOnPlayer = true;

            scoreRoutine = StartCoroutine(ScorePerSecond(player.GetComponent<PlayerController>().playerNr));
        }
    }

    public void ThrowHat()
    {
        if (isOnPlayer)
        {
            gameObject.transform.parent = null;
            spawnIndex = Random.Range(0, hatSpawns.Length);
            isLerping = true;
            if (scoreRoutine != null)
            {
                StopCoroutine(scoreRoutine);
                scoreRoutine = null;
            }
            StartCoroutine(SwitchBoolAfterSeconds());
        }
    }

    IEnumerator SwitchBoolAfterSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        isOnPlayer = false;
        yield return null;
    }

    IEnumerator ScorePerSecond(int playerNr)
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.AddScore(playerNr);

        if (isOnPlayer)
        {
            scoreRoutine = StartCoroutine(ScorePerSecond(playerNr));
        }    
        else
        {
            scoreRoutine = null;
        }
    }
}
