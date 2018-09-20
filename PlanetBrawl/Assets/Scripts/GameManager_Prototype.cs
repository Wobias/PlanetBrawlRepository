using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager_Prototype : MonoBehaviour
{
    public GameObject[] players;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            players[0].SetActive(true);
            players[1].SetActive(true);
            players[2].SetActive(false);
            players[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            players[0].SetActive(true);
            players[1].SetActive(true);
            players[2].SetActive(true);
            players[3].SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            players[0].SetActive(true);
            players[1].SetActive(true);
            players[2].SetActive(true);
            players[3].SetActive(true);
        }
    }
}
