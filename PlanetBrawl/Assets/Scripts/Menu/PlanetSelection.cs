using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSelection : MonoBehaviour
{
    public GameObject playerEarth;
    public GameObject playerDesert;
    public GameObject playerWater;
    public GameObject playerToxic;
    PlayerController playerController;
    public int playerNumber;
    public float cooldown = 0;


    private void Start()
    {
        cooldown = 0;
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            
            
            playerController = other.gameObject.GetComponent<PlayerController>();
            playerNumber = playerController.playerNr;

            if (transform.gameObject.tag == "Earth" && cooldown == 0)
            {
                GameObject newEarth = Instantiate(playerEarth, other.transform.position, Quaternion.identity);
                newEarth.layer = other.gameObject.layer;
                newEarth.GetComponent<PlayerController>().playerNr = playerNumber;
                Destroy(other.gameObject);
                cooldown += 5;
            }
            else if (transform.gameObject.tag == "Desert" && cooldown == 0)
            {
                GameObject newDesert = Instantiate(playerDesert, other.transform.position, Quaternion.identity);
                newDesert.layer = other.gameObject.layer;
                newDesert.GetComponent<PlayerController>().playerNr = playerNumber;
                Destroy(other.gameObject);
                cooldown += 5;

            }
            else if (transform.gameObject.tag == "Water" && cooldown == 0)
            {
                GameObject newWater = Instantiate(playerWater, other.transform.position, Quaternion.identity);
                newWater.layer = other.gameObject.layer;
                newWater.GetComponent<PlayerController>().playerNr = playerNumber;
                Destroy(other.gameObject);
                cooldown += 5;

            }
            else if (transform.gameObject.tag == "Toxic" && cooldown == 0)
            {
                GameObject newToxic = Instantiate(playerToxic, other.transform.position, Quaternion.identity);
                newToxic.layer = other.gameObject.layer;
                newToxic.GetComponent<PlayerController>().playerNr = playerNumber;
                Destroy(other.gameObject);
                cooldown += 5;

            }
        }

    }
}
