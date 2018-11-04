using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    public GameObject characterPrefab;
    public Color[] playerColors;

    private float cooldown = 0;

    int playerNumber;

    PlayerController playerController;
    MenuManager menuManager;


    private void Start()
    {
        cooldown = 0;
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void Update()
    {
        if(cooldown > 0.0f)
        {
            cooldown -= Time.deltaTime;
            if(cooldown < 0.0f)
            {
                cooldown = 0;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldown == 0)
        {
            playerController = other.transform.root.GetComponent<PlayerController>();

            if (playerController == null)
                return;

            playerNumber = playerController.playerNr;

            GameObject newPlanet = Instantiate(characterPrefab, other.transform.root.position, Quaternion.identity);
            SetLayer(newPlanet.transform, other.transform.root.gameObject.layer);
            newPlanet.GetComponent<PlayerController>().playerNr = playerNumber;
            newPlanet.GetComponent<PlayerController>().playerColor = playerColors[playerNumber-1];
            newPlanet.GetComponent<HealthController>().invincible = true;
            Destroy(other.transform.root.gameObject);
            cooldown += 1;

            menuManager.playerPrefabs[playerNumber-1] = characterPrefab;
        }
    }

    private void SetLayer(Transform root, int layer)
    {
        root.gameObject.layer = layer;
        foreach (Transform child in root)
            SetLayer(child, layer);
    }
}
