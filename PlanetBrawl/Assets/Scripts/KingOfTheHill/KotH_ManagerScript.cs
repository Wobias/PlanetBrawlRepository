using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class KotH_ManagerScript : MonoBehaviour
{
    public float pointsPlayer1;
    public float pointsPlayer2;
    public float pointsPlayer3;
    public float pointsPlayer4;

    public int winScore = 50;

    public Text pointsPlayer1Text;
    public Text pointsPlayer2Text;
    public Text pointsPlayer3Text;
    public Text pointsPlayer4Text;

    bool playerOneIn = false;
    bool playerTwoIn = false;
    bool playerThreeIn = false;
    bool playerFourIn = false;

    // für die LayerMask: zum suchen der Spieler in den Layern, 
    //zb Spieler1 hat den Layer: "Player1" 
    public string playerLayer = "Player";


    void Start ()
    {
		
	}
	
	void FixedUpdate ()
    {
		if (playerOneIn)
        {
            pointsPlayer1 += Time.fixedDeltaTime;
            pointsPlayer1Text.text = "Player1: " + Mathf.FloorToInt(pointsPlayer1).ToString();
            if (pointsPlayer1 >= winScore)
                SceneManager.LoadScene(0);
        }
        if (playerTwoIn)
        {
            pointsPlayer2 += Time.fixedDeltaTime;
            pointsPlayer2Text.text = "Player2: " + Mathf.FloorToInt(pointsPlayer1).ToString();
            if (pointsPlayer2 >= winScore)
                SceneManager.LoadScene(0);
        }
        if (playerThreeIn)
        {
            pointsPlayer3 += Time.fixedDeltaTime;
            pointsPlayer3Text.text = "Player3: " + Mathf.FloorToInt(pointsPlayer1).ToString();
            if (pointsPlayer3 >= winScore)
                SceneManager.LoadScene(0);
        }
        if (playerFourIn)
        {
            pointsPlayer4 += Time.fixedDeltaTime;
            pointsPlayer4Text.text = "Player4: " + Mathf.FloorToInt(pointsPlayer1).ToString();
            if (pointsPlayer4 >= winScore)
                SceneManager.LoadScene(0);
        }
    }
    public void PlayerEnter(int newlayer)
    {
        if (newlayer == LayerMask.NameToLayer(playerLayer+1))
        {
            playerOneIn = true;
        }
        else if(newlayer == LayerMask.NameToLayer(playerLayer + 2))
        {
            playerTwoIn = true;
        }
        else if(newlayer == LayerMask.NameToLayer(playerLayer + 3))
        {
            playerThreeIn = true;
        }
        else if(newlayer == LayerMask.NameToLayer(playerLayer + 4))
        {
            playerFourIn = true;
        }
    }
    public void PlayerExit(int newlayer)
    {
        if (newlayer == LayerMask.NameToLayer(playerLayer + 1))
        {
            playerOneIn = false;
        }
        else if (newlayer == LayerMask.NameToLayer(playerLayer + 2))
        {
            playerTwoIn = false;
        }
        else if (newlayer == LayerMask.NameToLayer(playerLayer + 3))
        {
            playerThreeIn = false;
        }
        else if (newlayer == LayerMask.NameToLayer(playerLayer + 4))
        {
            playerFourIn = false;
        }
    }
}
