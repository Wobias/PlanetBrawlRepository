using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HillTrigger : MonoBehaviour
{
    public float hillTimer = 8f;
    private float hillResetTimer = 10f;
    public float minSize;

    Vector3 startSize;
    Vector3 minSizeVector;
    Vector2 hillPosition;
    SpriteRenderer rend;

    Coroutine scoreRoutine;

    List<int> insidePlayers = new List<int>();


    void Start()
    {
        minSizeVector = new Vector3(minSize, minSize, minSize);
        transform.position = new Vector2(0, 0);
        startSize = transform.localScale;
        rend = GetComponent<SpriteRenderer>();
        //rend.color = Color.green; 
    }

    // Update is called once per frame
    void Update()
    {
        if (hillTimer <= hillResetTimer / 2)
            transform.localScale = Vector3.Lerp(transform.localScale, minSizeVector, Time.deltaTime / hillTimer);

        hillTimer -= Time.deltaTime;

        if (hillTimer <= 0f)
        {
            hillResetTimer = Random.Range(5f, 15f);
            hillTimer = hillResetTimer;
            SetPortal();
        }
    }

    void SetPortal()
    {
        //Reichweite anpassen
        hillPosition.x = Random.Range(-18f, 19f);
        hillPosition.y = Random.Range(-8f, 9f);

        transform.position = hillPosition;
        transform.localScale = startSize;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        insidePlayers.Add(other.transform.root.GetComponent<PlayerController>().playerNr);
        CheckForDomPlayer();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        insidePlayers.Remove(other.transform.root.GetComponent<PlayerController>().playerNr);
        CheckForDomPlayer();
    }

    void CheckForDomPlayer()
    {
        if (insidePlayers.Count == 1)
        {
            if (scoreRoutine != null)
                StopCoroutine(scoreRoutine);

            scoreRoutine = StartCoroutine(AddScorePerSecond(insidePlayers[0]));
            rend.color = GameManager.instance.GetColor(insidePlayers[0]);
        }
        else if (scoreRoutine != null)   
        {
            StopCoroutine(scoreRoutine);
            scoreRoutine = null;

            rend.color = Color.white;
        }
    }

    IEnumerator AddScorePerSecond(int playerNr)
    {
        yield return new WaitForSeconds(1);
        GameManager.instance.AddScore(playerNr);

        if (scoreRoutine != null)
            scoreRoutine = StartCoroutine(AddScorePerSecond(playerNr));
    }
}
