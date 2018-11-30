using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public Transform[] playerSpawns;


    private void Start()
    {
        FindObjectOfType<PlayerUI>().InitUI(GameManager.instance.SpawnPlayers(playerSpawns, true));
    }
}
