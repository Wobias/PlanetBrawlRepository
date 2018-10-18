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


    public GameObject victoryScreen;

    public bool teamMode;
    public bool gameOver;

    private void Awake()
    {
        gameManager = this;

        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
        

        foreach (var player in GameObject.FindGameObjectsWithTag("Player"))
        {
            players.Add(player);
        }

        if (teamMode == true)
        {
            //players[0].layer = 8;
            //players[1].layer = 8;
            //players[2].layer = 9;
            //players[3].layer = 9;

            foreach (var player in players)
            {
                if (player != null)
                {
                    if (player.layer == 8)
                    {
                        teamOne.Add(player);
                    }
                    else if (player.layer == 9)
                    {
                        teamTwo.Add(player);
                    }
                }
            }
        }
        //else if (teamMode == false)
        //{
        //    players[0].layer = 8;
        //    players[1].layer = 9;
        //    players[2].layer = 10;
        //    players[3].layer = 11;

        //    for (int i = 0; i < players.Count; i++)
        //    {
        //        SetLayer(players[i].transform, players[i].layer);
        //    }
        //}
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Destroy(players[2]);
            if (players[3] != null)
                Destroy(players[3]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(players[3]);
        }

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
                victoryScreen.SetActive(true);
            }
            else if (teamTwo.Count <= 0)
            {
                Debug.Log("Team One is victorious");
                victoryScreen.SetActive(true);
            }
        }
        else if (!teamMode)
        {
            if (players != null)
            {
                if (players.Count == 1)
                {
                    Debug.Log(players[0] + " " + " is victorious");
                    victoryScreen.SetActive(true);
                }
            }
        }
    }
}

