using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryUI : MonoBehaviour
{

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void StartFreeForAll()
    {
        SceneManager.LoadScene(1);
    }

    public void StartTeamFight()
    {
        SceneManager.LoadScene(2);
    }
}
