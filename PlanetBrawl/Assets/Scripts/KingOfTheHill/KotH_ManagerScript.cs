using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class KotH_ManagerScript : MonoBehaviour, IModeController
{
    public GameObject hillPrefab;
    public GameObject countdownPrefab;
    public GameObject scorePrefab;
    public float timer;

    private Transform[] playerSpawns;
    private List<GameObject> players = new List<GameObject>();
    private int[] scores = new int[4];
    private TextMeshProUGUI countdown;
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI victoryText;
    private bool gameOver = false;


    public void InitMode(Transform[] spawns, Transform[] entitySpawns, GameObject[] playerPrefabs)
    {
        int spawnIndex = Random.Range(0, entitySpawns.Length);
        HillTrigger hill = Instantiate(hillPrefab, entitySpawns[spawnIndex].position, Quaternion.identity).GetComponent<HillTrigger>();
        hill.spawns = entitySpawns;

        playerSpawns = spawns;

        GameObject[] allPlayers = new GameObject[4];

        for (int i = 0; i < 4; i++)
        {
            if (playerPrefabs[i] != null)
            {
                GameObject newPlayer = Instantiate(playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
                newPlayer.GetComponent<PlayerController>().playerNr = i + 1;
                players.Add(newPlayer);
                allPlayers[i] = newPlayer;
            }
            else
            {
                allPlayers[i] = null;
            }
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(i + 1);
            GameManager.SetLayer(players[i].transform, LayerMask.NameToLayer("Player" + (i + 1)));
        }

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }

        countdown = Instantiate(countdownPrefab, victoryText.transform.root).GetComponent<TextMeshProUGUI>();
        scoreText = Instantiate(scorePrefab, victoryText.transform.root).GetComponent<TextMeshProUGUI>();

        scoreText.text = "";

        for (int i = 0; i < players.Count; i++)
        {
            scoreText.text += "0";

            if (i < players.Count - 1)
            {
                scoreText.text += " - ";
            }
        }

        PlayerUI playerUI = FindObjectOfType<PlayerUI>();
        playerUI?.InitUI(allPlayers);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver)
            return;

        timer -= Time.fixedDeltaTime;
        countdown.SetText(Mathf.RoundToInt(timer).ToString());

        if (timer <= 0f)
        {
            int bestScore = 0;
            int winner = 0;
            countdown.gameObject.SetActive(false);
            for (int i = 0; i < players.Count; i++)
            {
                if (scores[i] > bestScore)
                {
                    bestScore = scores[i];
                    winner = i;
                }
                else if (scores[i] == bestScore)
                {
                    winner = 4;
                }
            }

            if (bestScore == 0)
            {
                victoryText.SetText("The Hill has no King...");
            }
            else if (winner < 4)
            {
                victoryText.SetText(LayerMask.LayerToName(players[winner].layer) + " is king of the Hill!");
            }
            else
            {
                victoryText.SetText("DRAW!");
            }

            victoryText.transform.parent.gameObject.SetActive(true);

            gameOver = true;
        }
    }

    public void AddScore(int playerNr)
    {
        scores[playerNr - 1]++;

        scoreText.text = "";

        for (int i = 0; i < players.Count; i++)
        {
            scoreText.text += scores[i];

            if (i < players.Count - 1)
            {
                scoreText.text += " - ";
            }
        }
    }
}
