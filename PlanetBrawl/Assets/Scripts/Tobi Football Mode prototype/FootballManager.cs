using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FootballManager : MonoBehaviour, IModeController
{
    public float gameTime;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> team1 = new List<GameObject>();
    private List<GameObject> team2 = new List<GameObject>();
    private Transform[] playerSpawns;
    private int[] scores = new int[2];
    private TextMeshProUGUI victoryText;
    //private TextMeshProUGUI countdown;
    //private TextMeshProUGUI scoreText;
    private bool gameOver = false;
    private bool paused = false;


    void FixedUpdate()
    {
        if (gameOver || paused)
            return;

        gameTime -= Time.fixedDeltaTime;

        if (gameTime <= 0)
        {
            int bestScore = 0;
            int winner = 0;
            //countdown.SetText("");
            for (int i = 0; i < scores.Length; i++)
            {
                if (scores[i] > bestScore)
                {
                    bestScore = scores[i];
                    winner = i;
                }
                else if (scores[i] == bestScore)
                {
                    winner = 3;
                }
            }

            if (bestScore == 0 || winner > 2)
            {
                victoryText.SetText("DRAW!");
            }
            else
            {
                victoryText.SetText("Team " + (winner+1) + " wins!");
            }

            victoryText.transform.parent.gameObject.SetActive(true);
            //countdown.gameObject.SetActive(false);

            gameOver = true;
        }
    }

    public void AddScore(int playerNr)
    {
        int scoringTeam = 0;

        if (team2.Contains(players[playerNr - 1]))
            scoringTeam = 1;

        scores[scoringTeam]++;
    }

    public void InitMode(Transform[] spawns, GameObject[] playerPrefabs)
    {
        playerSpawns = spawns;

        for (int i = 0; i < 5; i++)
        {
            if (playerPrefabs[i] != null)
            {
                GameObject newPlayer = Instantiate(playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
                newPlayer.GetComponent<PlayerController>().playerNr = i + 1;
                players.Add(newPlayer);

                if (team1.Count < Mathf.CeilToInt(playerPrefabs.Length / 2))
                    team1.Add(newPlayer);
                else
                    team2.Add(newPlayer);
            }
        }

        for (int i = 0; i < team1.Count; i++)
        {
            team1[i].GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(1);
            GameManager.SetLayer(team1[i].transform, LayerMask.NameToLayer("Player" + (1)));
        }

        for (int i = 0; i < team2.Count; i++)
        {
            team2[i].GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(2);
            GameManager.SetLayer(team2[i].transform, LayerMask.NameToLayer("Player" + (2)));
        }

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }
    }

    public void PauseGame(bool isPaused)
    {
        paused = isPaused;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(!isPaused);
        }
    }
}