﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseGameScript : MonoBehaviour
{
    public GameObject pauseCanvasGame;
    public GameObject pauseCanvasLobby;
    Scene currentScene;
    public Button[] buttonsGame;
    public Button[] buttonsLobby;
    private int index = 0;
    private bool pressed = false;



    private void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        //Beta Lobby Pause
        if (currentScene.name == "BetaLobby")
        {
            //UP
            if ((InputSystem.DPadDown(DPad.Down, 0) || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) < 0) && !pressed)
            {
                pressed = true;
                index++;

                if (index >= buttonsLobby.Length)
                    index = 0;

                buttonsLobby[index].Select();
            }
            //Down
            if ((InputSystem.DPadDown(DPad.Up, 0) && index > 0 || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) > 0) && !pressed)
            {
                pressed = true;
                index--;

                if (index < 0)
                    index = buttonsLobby.Length-1;

                buttonsLobby[index].Select();
            }
            if (InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) == 0 && pressed)
            {
                pressed = false;
            }
        }

        //Press Start in Beta Lobby to open Pause Menu
        if (InputSystem.ButtonDown(Buttons.Start, 0) && currentScene.name == "BetaLobby")
        {
            pauseCanvasLobby.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            buttonsLobby[index].Select();
            GameManager.instance.PauseGame(true);


        }



        if (currentScene.name != "BetaLobby")
        {
            //UP
            if ((InputSystem.DPadDown(DPad.Down, 0) || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) < 0) && !pressed)
            {
                pressed = true;
                index++;

                if (index >= buttonsGame.Length)
                    index = 0;

                buttonsGame[index].Select();
            }
            //Down
            if ((InputSystem.DPadDown(DPad.Up, 0) && index > 0 || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) > 0) && !pressed)
            {
                pressed = true;
                index--;

                if (index < 0)
                    index = buttonsGame.Length-1;

                buttonsGame[index].Select();
            }
            if (InputSystem.ThumbstickInput(ThumbSticks.LeftY,0) == 0 && pressed)
            {
                pressed = false;
            }
        }

        //Press Start while Ingame
        if (InputSystem.ButtonDown(Buttons.Start, 0) && currentScene.name != "BetaLobby")
        {
            pauseCanvasGame.SetActive(true);
            EventSystem.current.SetSelectedGameObject(null);
            buttonsGame[index].Select();
            GameManager.instance.PauseGame(true);

        }



        if (pauseCanvasGame.activeSelf || pauseCanvasLobby.activeSelf)
        {
            //Buttons for Lobby
            if (InputSystem.ButtonDown(Buttons.A, 0) && pauseCanvasLobby.activeSelf)
            {
                buttonsLobby[index].onClick.Invoke();
            }

            //Buttons for Game
            if (InputSystem.ButtonDown(Buttons.A, 0) && pauseCanvasGame.activeSelf)
            {
                buttonsGame[index].onClick.Invoke();
            }
        }



    }

    //Close Game Function
    public void CloseGame()
    {
        Application.Quit();
    }

    public void RestartGame()
    {
        GameManager.instance.ResetGame();
    }

    public void ResumeGame()
    {
        if (pauseCanvasGame.activeSelf == true)
        {
            pauseCanvasGame.SetActive(false);
        }
        if (pauseCanvasLobby.activeSelf == true)
        {
            pauseCanvasLobby.SetActive(false);
        }
        index = 0;
        GameManager.instance.PauseGame(false);
    }


}
