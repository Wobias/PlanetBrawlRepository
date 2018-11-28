using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
	void Update ()
    {
		if (InputSystem.ButtonDown(Buttons.Start, 0) ||
            InputSystem.ButtonDown(Buttons.Start, 1) ||
            InputSystem.ButtonDown(Buttons.Start, 2) ||
            InputSystem.ButtonDown(Buttons.Start, 3))
        {
            SceneManager.LoadScene(1);
        }
	}
}
