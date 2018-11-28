using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using XInputDotNetPure;

public class VictoryUI : MonoBehaviour
{
    private int index = 0;
    private bool pressed = false;
    public Button[] victoryButtons;
    public GameObject victoryUI;


    private void Update()
    {
        //UP
        if (victoryUI.activeSelf)
        {
            Time.timeScale = 0.000001f;
            GamePad.SetVibration(PlayerIndex.One, 0, 0);
            GamePad.SetVibration(PlayerIndex.Two, 0, 0);
            GamePad.SetVibration(PlayerIndex.Three, 0, 0);
            GamePad.SetVibration(PlayerIndex.Four, 0, 0);
            if ((InputSystem.DPadDown(DPad.Down, 0) || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) < 0) && !pressed)
            {
                pressed = true;
                index++;

                if (index >= victoryButtons.Length)
                    index = 0;

                victoryButtons[index].Select();
            }
            //Down
            if ((InputSystem.DPadDown(DPad.Up, 0) && index > 0 || InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) > 0) && !pressed)
            {
                pressed = true;
                index--;

                if (index < 0)
                    index = victoryButtons.Length - 1;

                victoryButtons[index].Select();
            }

            if (InputSystem.ThumbstickInput(ThumbSticks.LeftY, 0) == 0 && pressed)
            {
                pressed = false;
            }
        }

        if (InputSystem.ButtonDown(Buttons.A, 0) &&  victoryUI.activeSelf)
        {
            victoryButtons[index].onClick.Invoke();
        }

    }



    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void GoToMenu()
    {
        GameManager.instance.ResetGame();
        Time.timeScale = 1;
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
