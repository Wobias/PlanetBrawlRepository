using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathmatchController : MonoBehaviour, IModeController
{
    public int winScore = 3;
    public GameObject scorePrefab;

    private List<GameObject> players = new List<GameObject>();
    private Transform[] playerSpawns;
    private int[] scores = new int[4];
    private TextMeshProUGUI victoryText;
    private TextMeshProUGUI scoreText;
    private PlayerUI playerUI;


    public void AddScore(int playerNr)
    {
        scores[playerNr - 1]++;

        //playerUI.SetKillCount(playerNr -1, scores[playerNr - 1]);

        scoreText.text = "";

        for (int i = 0; i < players.Count; i++)
        {
            scoreText.text += scores[i];

            if (i < players.Count - 1)
            {
                scoreText.text += " - ";
            }
        }

        if (scores[playerNr-1] >= winScore)
        {
            Debug.Log("Player " + playerNr + " won!");
            victoryText?.SetText("Player " + playerNr + " won!");
            victoryText?.transform.parent.gameObject.SetActive(true);
            GameObject.FindGameObjectWithTag("VictoryScreen")?.GetComponent<VictoryUI>().OnVictory();
            //foreach (var player in players)
            //{
            //    player.isStatic = true;
            //}
        }
    }

    public void InitMode(Transform[] spawns, Transform[] entitySpawns, GameObject[] playerPrefabs)
    {
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
            players[i].GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(i+1);
            GameManager.SetLayer(players[i].transform, LayerMask.NameToLayer("Player" + (i + 1)));
        }

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }

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

        playerUI = FindObjectOfType<PlayerUI>();
        playerUI?.InitUI(allPlayers);
    }
}
