using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameModes { deathmatch, teamdeathmatch, hatmode, survival, soccer, koth};

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Color[] playerColors;
    public GameObject[] playerPrefabs;
    public GameModes gameMode;
    public GameObject[] modeManagers;

    private int[] selectedPlanets = new int[4];
    private bool[] activePlayers = new bool[4];
    private IModeController modeController;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayer(int index, int playerNr)
    {
        selectedPlanets[playerNr - 1] = index;
        activePlayers[playerNr - 1] = true;
        Debug.Log("Player added: " + playerNr + ", " + activePlayers[playerNr - 1]);
    }

    public void RemovePlayer(int playerNr)
    {
        activePlayers[playerNr - 1] = false;
        Debug.Log("Player removed: " + playerNr + ", " + !activePlayers[playerNr - 1]);
    }

    public Color GetColor(int playerNr)
    {
        return playerColors[playerNr - 1];
    }

    public void SpawnPlayers(Transform[] spawnPositions, bool noDeaths=false)
    {
        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i])
            {
                PlayerController player = Instantiate(playerPrefabs[selectedPlanets[i]], spawnPositions[i].position, Quaternion.identity).GetComponent<PlayerController>();
                SetLayer(player.transform, LayerMask.NameToLayer("Player" + (i + 1)));
                player.playerColor = playerColors[i];
                player.playerNr = i + 1;
                player.GetComponent<Player_HealthController>().invincible = noDeaths;
            }
        }
    }

    public void StartGame(Transform[] playerSpawns)
    {
        List<GameObject> activePrefabs = new List<GameObject>();

        for (int i = 0; i < activePlayers.Length; i++)
        {
            if (activePlayers[i])
            {
                activePrefabs.Add(playerPrefabs[selectedPlanets[i]]);
            }
            else
            {
                activePrefabs.Add(null);
            }
        }

        modeController = Instantiate(modeManagers[(int)gameMode]).GetComponent<IModeController>();
        modeController.InitMode(playerSpawns, activePrefabs.ToArray());
    }

    public void AddScore(int playerNr)
    {
        modeController.AddScore(playerNr);
    }

    public static void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }

    public void ResetGame()
    {
        gameMode = GameModes.deathmatch;
        selectedPlanets = new int[4];
        activePlayers = new bool[4];
    }

    public void PauseGame(bool isPaused)
    {
        modeController.PauseGame(isPaused);
    }
}
