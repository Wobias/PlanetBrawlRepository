using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform[] playerSpawns;


    private void Start()
    {
        GameManager.instance.SpawnPlayers(playerSpawns, true);
    }
}
