using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalController : MonoBehaviour, IModeController
{
    public GameObject enemyPrefab;
    public float spawnTimeout;

    private float score;
    private List<GameObject> players = new List<GameObject>();
    private GameObject[] sortedPlayers;
    private Transform[] playerSpawns;
    private TextMeshProUGUI victoryText;
    //private TextMeshProUGUI scoreText;
    private float timeout;
    private bool gameOver = false;


    void FixedUpdate()
    {
        if (gameOver)
            return;

        score += Time.fixedDeltaTime;

        timeout -= Time.fixedDeltaTime;

        if (timeout <= 0)
        {
            timeout = spawnTimeout;
            Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    public void AddScore(int playerNr)
    {
        if (players.Count > 0)
        {
            players.Remove(sortedPlayers[playerNr-1]);
            Debug.Log(players.Count);
        }
        
        if (players.Count <= 0)
        {
            victoryText.SetText("You survived for: " + Mathf.FloorToInt(score) + "seconds");
            victoryText.transform.parent.gameObject.SetActive(true);
            //countdown.gameObject.SetActive(false);

            gameOver = true;
        }
    }

    public void InitMode(Transform[] spawns, GameObject[] playerPrefabs)
    {
        playerSpawns = spawns;

        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
            newPlayer.GetComponent<PlayerController>().playerNr = i + 1;
            newPlayer.GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(1);
            GameManager.SetLayer(newPlayer.transform, LayerMask.NameToLayer("Player1"));
            players.Add(newPlayer);
            Debug.Log(players.Count);
        }

        sortedPlayers = players.ToArray();

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }

        timeout = spawnTimeout;
        Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
    }
}
