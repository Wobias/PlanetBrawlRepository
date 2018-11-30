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
            StartCoroutine(LoadLobby());
        }
    }

    IEnumerator LoadLobby()
    {
        Fading.instance.FadeIn(0.1f);
        yield return new WaitForSeconds(0.5f);

        if (joinedPlayers == readyPlayers && joinedPlayers != 0)
        {
            SceneManager.LoadScene(2);
        }
        else
        {
            Fading.instance.FadeOut(0.1f);
        }
    }
}



