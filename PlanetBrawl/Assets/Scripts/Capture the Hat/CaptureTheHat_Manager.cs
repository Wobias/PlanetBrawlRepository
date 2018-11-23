using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptureTheHat_Manager : MonoBehaviour, IModeController
{
    public GameObject hatPrefab;
    public GameObject countdownPrefab;
    public int winScore = 20;
    public float timer;

    private Transform[] playerSpawns;
    private List <GameObject> players = new List<GameObject>();
    private int[] scores = new int[4];
    private TextMeshProUGUI countdown;
    private TextMeshProUGUI victoryText;
    private bool gameOver = false;
    private bool paused = false;


    public void InitMode(Transform[] spawns, GameObject[] playerPrefabs)
    {
        Instantiate(hatPrefab, Vector3.zero, Quaternion.identity);

        playerSpawns = spawns;

        for (int i = 0; i < playerPrefabs.Length; i++)
        {
            GameObject newPlayer = Instantiate(playerPrefabs[i], playerSpawns[i].position, Quaternion.identity);
            newPlayer.GetComponent<PlayerController>().playerNr = i + 1;
            players.Add(newPlayer);
        }

        for (int i = 0; i < players.Count; i++)
        {
            players[i].GetComponent<PlayerController>().playerColor = GameManager.instance.GetColor(i + 1);
            GameManager.SetLayer(players[i].transform, LayerMask.NameToLayer("Player" + (i + 1)));
        }

        victoryText = GameObject.FindGameObjectWithTag("VictoryScreen").transform.Find("Victory Text").GetComponent<TextMeshProUGUI>();

        if (victoryText != null)
        {
            victoryText.transform.parent.gameObject.SetActive(false);
        }

        countdown = Instantiate(countdownPrefab, FindObjectOfType<Canvas>().transform).GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver || paused)
            return;

        timer -= Time.fixedDeltaTime;
        countdown.SetText(Mathf.RoundToInt(timer).ToString());

        if (timer <= 0f)
        {
            int bestScore = 0;
            int winner = 0;
            countdown.SetText("");
            for (int i = 0; i < players.Count; i++)
            {
                if (scores[i] > bestScore)
                {
                    bestScore = scores[i];
                    winner = i;
                }
                else if (scores[i] == bestScore)
                {
                    winner = 4;
                }
            }

            if (bestScore == 0)
            {
                victoryText.SetText("Nobody wears the hat!");
            }
            else if (winner < 4)
            {
                victoryText.SetText(LayerMask.LayerToName(players[winner].layer) + " wears the hat!");
            }
            else
            {
                victoryText.SetText("DRAW!");
            }

            victoryText.transform.parent.gameObject.SetActive(true);
            countdown.gameObject.SetActive(false);

            gameOver = true;
        }
    }

    public void AddScore(int playerNr)
    {
        scores[playerNr - 1]++;

        if (scores[playerNr-1] >= winScore)
        {
            victoryText.SetText(LayerMask.LayerToName(players[playerNr-1].layer) + " wears the hat!");
            victoryText.transform.parent.gameObject.SetActive(true);
        }
    }

    public void PauseGame(bool isPaused)
    {
        paused = isPaused;

        for (int i = 0; i < players.Count; i++)
        {
            players[i].SetActive(!isPaused);
        }
    }
}
