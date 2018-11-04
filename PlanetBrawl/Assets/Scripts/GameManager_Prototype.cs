using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager_Prototype : MonoBehaviour
{
    public static GameManager_Prototype gameManager;

    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> teamOne = new List<GameObject>();
    public List<GameObject> teamTwo = new List<GameObject>();
    public Transform[] playerSpawns;

    public GameObject victoryScreen;

    public TextMeshProUGUI victoryText;

    public bool teamMode;
    public bool gameOver;

    public Color[] teamColors;

    private MenuManager menuManager;


    private void Awake()
    {
        gameManager = this;

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }


        menuManager = FindObjectOfType<MenuManager>();
        teamMode = menuManager.teamMode;

        for (int i = 0; i < 4; i++)
        {
            if (menuManager.playerNumbers[i] != 0)
            {
                GameObject newPlayer = Instantiate(menuManager.playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
                newPlayer.GetComponent<PlayerController>().playerNr = menuManager.playerNumbers[i];
                players.Add(newPlayer);
            }
        }

        Destroy(menuManager.gameObject);

        if (teamMode == true)
        {
            if (players.Count < 3)
            {
                SetLayer(players[0].transform, LayerMask.NameToLayer("Player1"));
                if (players.Count == 2)
                    SetLayer(players[1].transform, LayerMask.NameToLayer("Player2"));
            }
            else
            {
                SetLayer(players[0].transform, LayerMask.NameToLayer("Player1"));
                SetLayer(players[1].transform, LayerMask.NameToLayer("Player2"));
                SetLayer(players[2].transform, LayerMask.NameToLayer("Player3"));
                if (players.Count == 4)
                    SetLayer(players[3].transform, LayerMask.NameToLayer("Player4"));
            }

            foreach (var player in players)
            {
                if (player != null)
                {
                    if (player.layer == LayerMask.NameToLayer("Player1"))
                    {
                        teamOne.Add(player);
                        player.GetComponent<PlayerController>().playerColor = teamColors[0];
                    }
                    else if (player.layer == LayerMask.NameToLayer("Player2"))
                    {
                        teamTwo.Add(player);
                        player.GetComponent<PlayerController>().playerColor = teamColors[1];
                    }
                }
            }
        }
        else if (teamMode == false)
        {
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i] != null)
                {
                    players[i].GetComponent<PlayerController>().playerColor = teamColors[i];
                    SetLayer(players[i].transform, LayerMask.NameToLayer("Player" + (i+1).ToString()));
                }
            }
        }
    }

    void FixedUpdate()
    {
        VictoryConditions();
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public void RemoveFromLists(GameObject deadPlayer)
    {
        if (deadPlayer != null)
        {
            players.Remove(deadPlayer);

            if (teamMode)
            {
                if (teamOne.Contains(deadPlayer))
                {
                    teamOne.Remove(deadPlayer);
                }
                else if (teamTwo.Contains(deadPlayer))
                {
                    teamTwo.Remove(deadPlayer);
                }
            }
        }
    }

    public void VictoryConditions()
    {
        if (teamMode)
        {
            if (teamOne.Count <= 0)
            {
                Debug.Log("Team Two is victorious");
                victoryText.SetText("Team 2 won!");
                victoryScreen.SetActive(true);


                if (teamTwo.Count > 1)
                {
                    teamTwo[0].transform.position = new Vector3(-1.5f, 0f, 0f);
                    teamTwo[1].transform.position = new Vector3(1.5f, 0f, 0f);
                    teamTwo[0].isStatic = true;
                    teamTwo[1].isStatic = true;

                    foreach (var player in players)
                    {
                        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        player.GetComponent<PlayerController>().enabled = false;
                    }


                }
            }
            if (teamTwo.Count <= 0)
            {
                Debug.Log("Team One is victorious");
                victoryText.SetText("Team 1 won!");
                victoryScreen.SetActive(true);


                if (teamOne.Count > 1)
                {
                    teamOne[0].transform.position = new Vector3(-1f, 0f, 0f);
                    teamOne[1].transform.position = new Vector3(1f, 0f, 0f);
                    teamOne[0].isStatic = true;
                    teamOne[1].isStatic = true;

                    foreach (var player in players)
                    {
                        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        player.GetComponent<PlayerController>().enabled = false;
                    }


                }
            }
        }
        else if (!teamMode)
        {
            if (players != null)
            {
                if (players.Count == 1)
                {
                    Debug.Log(players[0].name + " is victorious");
                    victoryText.SetText("Player " + players[0].GetComponent<PlayerController>().playerNr + " won!");
                    victoryScreen.SetActive(true);

                    players[0].transform.position = new Vector3(0f, 0f, 0f);
                    players[0].isStatic = true;


                    foreach (var player in players)
                    {
                        player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        player.GetComponent<PlayerController>().enabled = false;
                    }
                }
            }
        }
    }
}



