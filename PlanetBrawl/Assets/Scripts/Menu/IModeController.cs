using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModeController
{
    void InitMode(Transform[] spawns, Transform[] entitySpawns, GameObject[] playerPrefabs);
    void AddScore(int playerNr);
}
