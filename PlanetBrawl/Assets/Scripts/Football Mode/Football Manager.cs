using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FootballManager : MonoBehaviour
{
    public TextMeshProUGUI victoryText;
    public TextMeshProUGUI scoreBoard;

    public GameObject victoryScreen;

    private static int teamOneScore;
    private static int teamTwoScore;


    // Use this for initialization
    void Start()
    {
        teamOneScore = 0;
        teamTwoScore = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreBoard.SetText(teamOneScore.ToString() + " " + "-" + " " + teamTwoScore.ToString());

        if (teamOneScore >= 3)
        {
            Debug.Log("Team One is victorious");
            victoryText.SetText("Team 1 won!");
            victoryScreen.SetActive(true);

            //teamOne[0].transform.position = new Vector3(-1f, 0f, 0f);
            //teamOne[1].transform.position = new Vector3(1f, 0f, 0f);
            //teamOne[0].isStatic = true;
            //teamOne[1].isStatic = true;

            //foreach (var player in players)
            //{
            //    player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            //    player.GetComponent<PlayerController>().enabled = false;
            //}
        }
    }
}



