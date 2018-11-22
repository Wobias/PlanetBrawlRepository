using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModeController
{
    void InitMode(Transform[] spawns, GameObject[] playerPrefabs);
    void AddScore(int playerNr);
}
