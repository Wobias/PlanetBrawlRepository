using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetSelectionManager : MonoBehaviour
{

    public GameObject[] startPrompts;
    public GameObject[] planetsP1;
    public GameObject[] planetsP2;
    public GameObject[] planetsP3;
    public GameObject[] planetsP4;
    public GameObject[] readyTextObjects;
    private int index1 = 0;
    private int index2 = 0;
    private int index3 = 0;
    private int index4 = 0;

    private bool isPressedP1 = false;
    private bool isPressedP2 = false;
    private bool isPressedP3 = false;
    private bool isPressedP4 = false;


    private bool isReadyP1 = false;
    private bool isReadyP2 = false;
    private bool isReadyP3 = false;
    private bool isReadyP4 = false;

    private int countConnectedPlayers;



    [HideInInspector]
    public int[] playerNumbers = new int[4];

    private void Start()
    {
       // DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //Player1 ready buutton
        if (InputSystem.ButtonDown(Button.A, 0) && playerNumbers[0] == 1 && !isReadyP1)
        {
            readyTextObjects[0].SetActive(true);
            isReadyP1 = true;
            Debug.Log(planetsP1[index1]);
        }
        if (InputSystem.ButtonDown(Button.B, 0) && playerNumbers[0] == 1 && isReadyP1)
        {
            readyTextObjects[0].SetActive(false);
            isReadyP1 = false;
            
        }


        //Player2 ready buutton
        if (InputSystem.ButtonDown(Button.A, 1) && playerNumbers[1] == 2 && !isReadyP2)
        {
            readyTextObjects[1].SetActive(true);
            isReadyP2 = true;
            Debug.Log(planetsP2[index2]);
        }
        if (InputSystem.ButtonDown(Button.B, 1) && playerNumbers[1] == 2 && isReadyP2)
        {
            readyTextObjects[1].SetActive(false);
            isReadyP2 = false;
        }



        //Player3 ready buutton
        if (InputSystem.ButtonDown(Button.A, 2) && playerNumbers[2] == 3 && !isReadyP3)
        {
            readyTextObjects[2].SetActive(true);
            isReadyP3 = true;
            Debug.Log(planetsP3[index3]);
        }
        if (InputSystem.ButtonDown(Button.B, 2) && playerNumbers[2] == 3 && isReadyP3)
        {
            readyTextObjects[2].SetActive(false);
            isReadyP3 = false;
        }





        //Player4 ready buutton
        if (InputSystem.ButtonDown(Button.A, 3) && playerNumbers[3] == 4 && !isReadyP4)
        {
            readyTextObjects[3].SetActive(true);
            isReadyP4 = true;
            Debug.Log(planetsP4[index4]);
        }
        if (InputSystem.ButtonDown(Button.B, 3) && playerNumbers[3] == 4 && isReadyP4)
        {
            readyTextObjects[3].SetActive(false);
            isReadyP4 = false;
        }


        if(countConnectedPlayers == 1 && isReadyP1 == true)
        {
            //Loadscene
            SceneManager.LoadScene("BetaLobby");
        }
        else if(countConnectedPlayers == 2 && isReadyP1 == true && isReadyP2 == true)
        {
            //LoadScene
            SceneManager.LoadScene("BetaLobby");
        }
        else if(countConnectedPlayers == 3 && isReadyP1 == true && isReadyP2 == true && isReadyP3 == true)
        {
            //LoadScene
            SceneManager.LoadScene("BetaLobby");
        }
        else if(countConnectedPlayers == 4 && isReadyP1 == true && isReadyP2 == true && isReadyP3 == true && isReadyP4 == true)
        {
            //LoadScene
            SceneManager.LoadScene("BetaLobby");
        }



        //Player 1 Join Button
        if (InputSystem.ButtonDown(Button.A, 0) && playerNumbers[0] == 0)
        {
            playerNumbers[0] = 1;
            startPrompts[0].SetActive(false);
            planetsP1[0].SetActive(true);
            countConnectedPlayers += 1;



        }

        //Player 2 Join Button
        if (InputSystem.ButtonDown(Button.A, 1) && playerNumbers[1] == 0)
        {
            playerNumbers[1] = 2;
            startPrompts[1].SetActive(false);
            planetsP2[0].SetActive(true);
            countConnectedPlayers += 1;

        }

        //Player 3 Join Button
        if (InputSystem.ButtonDown(Button.A, 2) && playerNumbers[2] == 0)
        {
            playerNumbers[2] = 3;
            startPrompts[2].SetActive(false);
            planetsP3[0].SetActive(true);
            countConnectedPlayers += 1;


        }

        //Player 4 Join Button
        if (InputSystem.ButtonDown(Button.A, 3) && playerNumbers[3] == 0)
        {
            playerNumbers[3] = 4;
            startPrompts[3].SetActive(false);
            planetsP4[0].SetActive(true);
            countConnectedPlayers += 1;
        }

        
        
        
        //Player1 
        //Switch planet to the right
        if (InputSystem.DPadDown(DPad.Right, 0) && playerNumbers[0] == 1 && !isReadyP1 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 0) > 0 && playerNumbers[0] == 1 && !isPressedP1 && !isReadyP1)
        {
            if (planetsP1[0].activeSelf == true)
            {
                planetsP1[0].SetActive(false);
            }
            if (index1 < planetsP1.Length - 1)
            {
                planetsP1[index1].SetActive(false);
                index1 += 1;
                if (index1 > planetsP1.Length)
                {
                    index1 += 1;
                }
                planetsP1[index1].SetActive(true);
                isPressedP1 = true;
            }

        }

        //Switch planet to the left
        if (InputSystem.DPadDown(DPad.Left, 0) && playerNumbers[0] == 1 && !isReadyP1 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 0) < 0 && playerNumbers[0] == 1 && !isPressedP1 && !isReadyP1)
        {
            if (planetsP1[0].activeSelf == true)
            {
                planetsP1[0].SetActive(false);
            }
            if (index1 < planetsP1.Length)
            {
                if (index1 >= 1)
                {
                    planetsP1[index1].SetActive(false);
                    index1 -= 1;
                }
                if (index1 > planetsP1.Length)
                {
                    index1 -= 1;
                }
                planetsP1[index1].SetActive(true);
                isPressedP1 = true;
            }

        }
        //Avoid skippit Planets while holding ThumbStick
        if (InputSystem.ThumbstickInput(ThumbStick.LeftX, 0) == 0)
        {
            isPressedP1 = false;
        }



        //    Player2
        //Switch planet to the right
        if (InputSystem.DPadDown(DPad.Right, 1) && playerNumbers[1] == 2 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 1) > 0 && playerNumbers[1] == 2 && !isPressedP2)
        {
            if (planetsP2[0].activeSelf == true)
            {
                planetsP2[0].SetActive(false);
            }
            if (index2 < planetsP2.Length - 1)
            {
                planetsP2[index2].SetActive(false);
                index2 += 1;
                if (index2 > planetsP2.Length)
                {
                    index2 += 1;
                }
                planetsP2[index2].SetActive(true);
                isPressedP2 = true;
            }

        }
        //Switch planet to the left
        if (InputSystem.DPadDown(DPad.Left, 1) && playerNumbers[1] == 2 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 1) < 0 && playerNumbers[1] == 2 && !isPressedP2)
        {
            if (planetsP2[0].activeSelf == true)
            {
                planetsP2[0].SetActive(false);
            }
            if (index2 < planetsP2.Length)
            {
                if (index2 >= 1)
                {
                    planetsP2[index2].SetActive(false);
                    index2 -= 1;
                }
                if (index2 > planetsP2.Length)
                {
                    index2 -= 1;
                }
                planetsP2[index2].SetActive(true);
                isPressedP2 = true;
            }

        }

        if (InputSystem.ThumbstickInput(ThumbStick.LeftX, 1) == 0)
        {
            isPressedP2 = false;
        }



        //    Player3
        //Switch planet to the right
        if (InputSystem.DPadDown(DPad.Right, 2) && playerNumbers[2] == 3 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 2) > 0 && playerNumbers[2] == 3 && !isPressedP3)
        {
            if (planetsP3[0].activeSelf == true)
            {
                planetsP3[0].SetActive(false);
            }
            if (index3 < planetsP3.Length - 1)
            {
                planetsP3[index3].SetActive(false);
                index3 += 1;
                if (index3 > planetsP3.Length)
                {
                    index3 += 1;
                }
                planetsP3[index3].SetActive(true);
                isPressedP3 = true;
            }

        }
        //Switch planet to the left
        if (InputSystem.DPadDown(DPad.Left, 2) && playerNumbers[2] == 3 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 2) < 0 && playerNumbers[2] == 3 && !isPressedP3)
        {
            if (planetsP3[0].activeSelf == true)
            {
                planetsP3[0].SetActive(false);
            }
            if (index3 < planetsP3.Length)
            {
                if (index3 >= 1)
                {
                    planetsP3[index3].SetActive(false);
                    index3 -= 1;
                }
                if (index3 > planetsP3.Length)
                {
                    index3 -= 1;
                }
                planetsP3[index3].SetActive(true);
                isPressedP3 = true;
            }

        }

        if (InputSystem.ThumbstickInput(ThumbStick.LeftX, 2) == 0)
        {
            isPressedP3 = false;
        }





        //    Player4
        //Switch planet to the right
        if (InputSystem.DPadDown(DPad.Right, 3) && playerNumbers[3] == 4 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 3) > 0 && playerNumbers[3] == 4 && !isPressedP4)
        {
            if (planetsP4[0].activeSelf == true)
            {
                planetsP4[0].SetActive(false);
            }
            if (index4 < planetsP4.Length - 1)
            {
                planetsP4[index4].SetActive(false);
                index4 += 1;
                if (index4 > planetsP4.Length)
                {
                    index4 += 1;
                }
                planetsP4[index4].SetActive(true);
                isPressedP4 = true;
            }

        }
        //Switch planet to the left
        if (InputSystem.DPadDown(DPad.Left, 3) && playerNumbers[3] == 4 || InputSystem.ThumbstickInput(ThumbStick.LeftX, 3) < 0 && playerNumbers[3] == 4 && !isPressedP4)
        {
            if (planetsP4[0].activeSelf == true)
            {
                planetsP4[0].SetActive(false);
            }
            if (index4 < planetsP4.Length)
            {
                if (index4 >= 1)
                {
                    planetsP4[index4].SetActive(false);
                    index4 -= 1;
                }
                if (index4 > planetsP4.Length)
                {
                    index4 -= 1;
                }
                planetsP4[index3].SetActive(true);
                isPressedP4 = true;
            }

        }

        if (InputSystem.ThumbstickInput(ThumbStick.LeftX, 2) == 0)
        {
            isPressedP3 = false;
        }

    }
}



