using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CaptureTheHat_Manager : MonoBehaviour
{
    public Camera camera;
    public GameObject hat;
    public GameObject victoryScreen;
    private GameObject[] gameObjects;
    private List <GameObject> players = new List<GameObject>();
    private float timer = 30f;
    public TextMeshProUGUI countdown;
    public TextMeshProUGUI victoryText;


    // Use this for initialization
    void Start()
    {
        players.AddRange(GameObject.FindGameObjectsWithTag("Player"));
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
        if (timer <= 0f && hat.layer != 0)
        {
            countdown.SetText("");
            VictoryConditions(players);
        }
    }

    private void VictoryConditions(List <GameObject> _players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (_players[i].layer != hat.layer)
            {
                _players[i].SetActive(false);
                _players.Remove(_players[i]);
            }
        }
        _players[0].transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, 0f);
        victoryText.SetText(LayerMask.LayerToName(players[0].layer) + " wears the hat!");
        victoryScreen.SetActive(true);
    }
}
