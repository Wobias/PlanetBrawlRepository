using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DeathmatchController : MonoBehaviour, IModeController
{
    public int winScore = 3;

    private List<GameObject> players = new List<GameObject>();
    private Transform[] playerSpawns;
    private int[] scores = new int[4];
    private TextMeshProUGUI victoryText;


    public void AddScore(int playerNr)
    {
        scores[playerNr - 1]++;

        if (scores[playerNr-1] >= winScore)
        {
            Debug.Log("Player " + playerNr + " won!");
            victoryText?.SetText("Player " + playerNr + " won!");
            victoryText?.transform.parent.gameObject.SetActive(true);

            //foreach (var player in players)
            //{
            //    player.isStatic = true;
            //}
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
                players.Add(newPlayer);
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
    }
}
