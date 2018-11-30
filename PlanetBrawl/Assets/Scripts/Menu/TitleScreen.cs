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
            StartCoroutine(LoadSelection());
        }
	}

    IEnumerator LoadSelection()
    {
        Fading.instance.FadeIn(0.1f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(1);
    }
}
