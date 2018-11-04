using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEntrance : MonoBehaviour
{
    public string connectedLevel;

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(connectedLevel);
    }
}
