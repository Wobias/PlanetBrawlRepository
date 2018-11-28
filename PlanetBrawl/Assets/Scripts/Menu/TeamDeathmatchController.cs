using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeamDeathmatchController : MonoBehaviour, IModeController
{
    public int winScore = 3;

    private List<GameObject> players = new List<GameObject>();
    private List<GameObject> team1 = new List<GameObject>();
    private List<GameObject> team2 = new List<GameObject>();
    private Transform[] playerSpawns;
    private int[] scores = new int[2];
    private TextMeshProUGUI victoryText;


    public void AddScore(int playerNr)
    {
        int scoringTeam = 0;

        if (team2.Contains(players[playerNr - 1]))
            scoringTeam = 1;

        scores[scoringTeam]++;

        if (scores[scoringTeam] >= winScore)
        {
            Debug.Log("Team " + (scoringTeam + 1) + " won!");
            victoryText?.SetText("Team " + (scoringTeam + 1) + " won!");
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
}
