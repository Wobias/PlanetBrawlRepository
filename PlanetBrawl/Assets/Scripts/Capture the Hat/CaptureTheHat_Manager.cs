using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptureTheHat_Manager : MonoBehaviour
{
    public GameObject hat;
    public GameObject victoryScreen;
    private GameObject[] gameObjects;
    private GameObject[] players;
    private float timer = 30f;
    public TextMeshProUGUI countdown;
    public TextMeshProUGUI victoryText;


    // Use this for initialization
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        gameObjects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].layer == 23)
            {
                gameObjects[i].SetActive(false);
            }
        }
        timer = 10f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        countdown.SetText(Mathf.RoundToInt(timer).ToString());
        if (timer <= 0f)
        {
            countdown.SetText("");
            VictoryConditions(hat.layer);
        }

    }

    private void VictoryConditions(int hatLayerNumber)
    {
        foreach (GameObject player in players)
        {
            if (player.layer == hatLayerNumber)
            {
                victoryScreen.SetActive(true);
                victoryText.SetText(player.tag + " wears the hat!");
            }
        }
    }
}
