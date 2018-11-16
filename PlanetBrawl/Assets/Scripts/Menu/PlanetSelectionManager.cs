using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelectionManager : MonoBehaviour
{
    public static PlanetSelectionManager instance;

    private int joinedPlayers = 0;
    private int readyPlayers = 0;


    void Awake()
    {
        instance = this;
    }

    public void JoinGame()
    {
        joinedPlayers++;
        CheckForGameStart();
    }

    public void LeaveGame()
    {
        joinedPlayers--;
        CheckForGameStart();
    }

    public void Ready()
    {
        readyPlayers++;
        CheckForGameStart();
    }

    public void Unready()
    {
        readyPlayers--;
        CheckForGameStart();
    }

    void CheckForGameStart()
    {
        if (joinedPlayers == readyPlayers && joinedPlayers != 0)
        {
            SceneManager.LoadScene(1);
        }
    }
}



