using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{

    public GameObject Earth;
    public GameObject Desert;
    public GameObject Water;
    public GameObject Toxic;
    PlayerController playerController;
    public int playerNumber;

    private void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();
        playerNumber = playerController.playerNr;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Earth")
        {
            
           GameObject newEarth = Instantiate(Earth, transform.position, Quaternion.identity);
            newEarth.layer = gameObject.layer;
            newEarth.GetComponent<PlayerController>().playerNr = playerNumber;
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Desert")
        {
            GameObject newDesert = Instantiate(Desert, transform.position, Quaternion.identity);
            newDesert.layer = gameObject.layer;
            newDesert.GetComponent<PlayerController>().playerNr = playerNumber;
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Water")
        {
            GameObject newWater = Instantiate(Water, transform.position, Quaternion.identity);
            newWater.layer = gameObject.layer;
            newWater.GetComponent<PlayerController>().playerNr = playerNumber;
            Destroy(gameObject);
        }

        else if (other.gameObject.tag == "Toxic")
        {
            GameObject newToxic = Instantiate(Toxic, transform.position, Quaternion.identity);
            newToxic.layer = gameObject.layer;
            newToxic.GetComponent<PlayerController>().playerNr = playerNumber;
            Destroy(gameObject);
        }

    }

}
