using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSpawner : MonoBehaviour
{
    public Transform[] playerSpawns;

    private void Start()
    {
        GameManager.instance.StartGame(playerSpawns);
    }
}
