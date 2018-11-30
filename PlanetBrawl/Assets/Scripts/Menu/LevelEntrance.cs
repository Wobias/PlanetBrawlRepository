using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour
{
    public string connectedLevel;
    private Coroutine loadRoutine;


    void OnTriggerEnter2D(Collider2D other)
    {
        loadRoutine = StartCoroutine(LoadLevel());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        StopCoroutine(loadRoutine);
        Fading.instance.FadeOut(0.1f);
    }

    IEnumerator LoadLevel()
    {
        Fading.instance.FadeIn(0.1f);
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(connectedLevel);
    }
}
