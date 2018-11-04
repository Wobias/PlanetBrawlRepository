using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject[] playerPrefabs;
    public Color[] playerColors;
    public GameObject[] startPrompts;
    public Transform[] playerSpawns;

    [HideInInspector]
    public int[] playerNumbers = new int[4];
    [HideInInspector]
    public bool teamMode = false;


    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetButtonDown("PlayerStart1") && playerNumbers[0] == 0)
        {
            playerNumbers[0] = 1;
            PlayerController player = Instantiate(playerPrefabs[0], playerSpawns[0].position, Quaternion.identity).GetComponent<PlayerController>();
            player.playerNr = 1;
            player.playerColor = playerColors[0];
            player.GetComponent<HealthController>().invincible = true;
            startPrompts[0].SetActive(false);
        }

        if (Input.GetButtonDown("PlayerStart2") && playerNumbers[1] == 0)
        {
            playerNumbers[1] = 2;
            PlayerController player = Instantiate(playerPrefabs[1], playerSpawns[1].position, Quaternion.identity).GetComponent<PlayerController>();
            player.playerNr = 2;
            player.playerColor = playerColors[1];
            player.GetComponent<HealthController>().invincible = true;
            startPrompts[1].SetActive(false);
        }

        if (Input.GetButtonDown("PlayerStart3") && playerNumbers[2] == 0)
        {
            playerNumbers[2] = 3;
            PlayerController player = Instantiate(playerPrefabs[2], playerSpawns[2].position, Quaternion.identity).GetComponent<PlayerController>();
            player.playerNr = 3;
            player.playerColor = playerColors[2];
            player.GetComponent<HealthController>().invincible = true;
            startPrompts[2].SetActive(false);
        }

        if (Input.GetButtonDown("PlayerStart4") && playerNumbers[3] == 0)
        {
            playerNumbers[3] = 4;
            PlayerController player = Instantiate(playerPrefabs[3], playerSpawns[3].position, Quaternion.identity).GetComponent<PlayerController>();
            player.playerNr = 4;
            player.playerColor = playerColors[3];
            player.GetComponent<HealthController>().invincible = true;
            startPrompts[3].SetActive(false);
        }
    }

    public void SwitchTeamMode(bool teamActive)
    {
        teamMode = teamActive;
    }
}
