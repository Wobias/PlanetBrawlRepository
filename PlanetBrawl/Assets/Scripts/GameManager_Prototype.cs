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
            Destroy(players[2]);
            if (players[3] != null)
                Destroy(players[3]);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Destroy(players[3]);
        }
    }
}
