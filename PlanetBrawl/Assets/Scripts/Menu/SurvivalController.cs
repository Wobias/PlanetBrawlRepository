using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SurvivalController : MonoBehaviour, IModeController
{
    public GameObject enemyPrefab;
    public float spawnTimeout;
    public GameObject scorePrefab;

    private float score;
    private int playerCount = 0;
    private GameObject[] players = new GameObject[4];
    private Transform[] playerSpawns;
    private Transform[] enemySpawns;
    private TextMeshProUGUI victoryText;
    private TextMeshProUGUI scoreText;
    private float timeout;
    private bool gameOver = false;
    private bool paused = false;
    private PlayerUI playerUI;


    void FixedUpdate()
    {
        if (gameOver || paused)
            return;

        score += Time.fixedDeltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString();

        timeout -= Time.fixedDeltaTime;

        if (timeout <= 0)
        {
            timeout = spawnTimeout;
            int spawnIndex = Random.Range(0, enemySpawns.Length);
            Instantiate(enemyPrefab, enemySpawns[spawnIndex].position, Quaternion.identity);
        }
    }

    public void AddScore(int playerNr)
    {
        if (playerCount > 0)
        {
            players[playerNr - 1] = null;
            playerCount--;
            Debug.Log(playerCount);
        }
        
        if (playerCount <= 0)
        {
            scoreText.gameObject.SetActive(false);
            victoryText.SetText("You survived for: " + Mathf.FloorToInt(score) + " seconds");
            victoryText.transform.parent.gameObject.SetActive(true);
            //countdown.gameObject.SetActive(false);

            gameOver = true;
        }
    }

    public void InitMode(Transform[] spawns, Transform[] entitySpawns, GameObject[] playerPrefabs)
    {
        playerSpawns = spawns;

        for (int i = 0; i < 4; i++)
        {
            if (playerPrefabs[i] != null)
            {
                GameObject newPlayer = Instantiate(playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
                newPlayer.GetComponent<PlayerController>().playerNr = i + 1;
                newPlayer.GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(1);
                GameManager.SetLayer(newPlayer.transform, LayerMask.NameToLayer("Player1"));
                players[i] = newPlayer;
                playerCount++;
            }
            else
            {
                players[i] = null;
            }
        }

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }

        scoreText = Instantiate(scorePrefab, victoryText.transform.root).GetComponent<TextMeshProUGUI>();

        timeout = spawnTimeout;
        int spawnIndex = Random.Range(0, entitySpawns.Length);
        Instantiate(enemyPrefab, entitySpawns[spawnIndex].position, Quaternion.identity);

        enemySpawns = entitySpawns;

        playerUI = FindObjectOfType<PlayerUI>();
        playerUI?.InitUI(players);
    }

    public void PauseGame(bool isPaused)
    {
        paused = isPaused;

        for (int i = 0; i < playerCount; i++)
        {
            players[i].SetActive(!isPaused);
        }
    }
}
