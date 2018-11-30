using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    public int playerNr = 1;
    public GameObject[] characters;
    public GameObject joinText;
    public GameObject readyButton;
    public GameObject readyText;

    private Color playerColor;
    private int index = 0;
    private bool pressed = false;
    private bool joined = false;
    private bool ready = false;


    private void Start()
    {
        playerColor = GameManager.instance.GetColor(playerNr);

        for (int i = 0; i < characters.Length; i++)
        {
            characters[i].transform.Find("Outline").GetComponent<SpriteRenderer>().color = playerColor;
        }
    }

    private void Update()
    {
        if (ready)
        {
            if (InputSystem.ButtonDown(Buttons.B, playerNr-1) ||
                !InputSystem.PadConnected(playerNr-1))
            {
                ready = false;
                GameManager.instance.RemovePlayer(playerNr);
                PlanetSelectionManager.instance.Unready();
                readyText.SetActive(false);
                readyButton.SetActive(true);
            }

            return;
        }

        if (!joined && InputSystem.ButtonDown(Buttons.A, playerNr-1))
        {
            joined = true;
            characters[index].SetActive(true);
            PlanetSelectionManager.instance.JoinGame();
            joinText.SetActive(false);
            readyButton.SetActive(true);
        }
        else if (joined)
        {
            if (InputSystem.ButtonDown(Buttons.A, playerNr - 1))
            {
                ready = true;
                GameManager.instance.SetPlayer(index, playerNr);
                PlanetSelectionManager.instance.Ready();
                readyButton.SetActive(false);
                readyText.SetActive(true);
                return;
            }
            else if (InputSystem.ButtonDown(Buttons.B, playerNr - 1) ||
                !InputSystem.PadConnected(playerNr - 1))
            {
                joined = false;
                PlanetSelectionManager.instance.LeaveGame();
                characters[index].SetActive(false);
                readyButton.SetActive(false);
                joinText.SetActive(true);
                return;
            }

            if (InputSystem.DPadDown(DPad.Right, playerNr - 1) ||
            InputSystem.ThumbstickInput(ThumbSticks.LeftX, playerNr - 1) > 0 && !pressed)
            {
                pressed = true;
                characters[index].SetActive(false);
                index++;

                if (index >= characters.Length)
                    index = 0;

                characters[index].SetActive(true);
            }
            else if (InputSystem.DPadDown(DPad.Left, playerNr - 1) ||
                InputSystem.ThumbstickInput(ThumbSticks.LeftX, playerNr - 1) < 0 && !pressed)
            {
                pressed = true;
                characters[index].SetActive(false);
                index--;

                if (index < 0)
                    index = characters.Length - 1;

                characters[index].SetActive(true);
            }

            if (InputSystem.ThumbstickInput(ThumbSticks.LeftX, playerNr - 1) == 0 && pressed)
            {
                pressed = false;
            }
        } 
    }
}
