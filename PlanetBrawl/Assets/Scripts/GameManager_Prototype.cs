using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager_Prototype : MonoBehaviour
{
    public static GameManager_Prototype gameManager;
    public GameObject[] players;

    public TextMeshPro victoryText;

    public bool teamMode;

    private void Start()
    {
        gameManager = this;

        if (teamMode == true)
        {
            players[0].layer = 8;
            players[1].layer = 8;
            players[2].layer = 9;
            players[3].layer = 9;
        }
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
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    //public void CheckPlayersAlive()
    //{
    //    for (int i = 0; i < players.Length; i++)
    //    {
    //        players[i];
    //    }

    //}

    private void Victory()
    {


    }
}
