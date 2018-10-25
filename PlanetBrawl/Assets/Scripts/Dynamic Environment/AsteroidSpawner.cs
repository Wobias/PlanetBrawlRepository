using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public float spawnTimeout = 5;
    private float spawnTimer = 5;
    //public int spawnMin = 0;
    //public int spawnMax = 1000;
    public GameObject asteroid;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public int whichBorder;


    void Update()
    {
        /* Spawn asteroid with a random chance */
        //float sum = Random.Range(spawnMin, spawnMax);
        //Debug.Log(sum);
        //if (sum == 50)
        //{
        //    Invoke("SpawnMeteor", 1);

        //}


        /*Spawn asteroid after Time*/
        spawnTimer -= Time.deltaTime;
       // Debug.Log(spawnTimer);
        if (spawnTimer < 0)
        {

            spawnTimer = spawnTimeout;
        }

        if (spawnTimer == spawnTimeout)
        {
            SpawnMeteor();
        }




    }

    void SpawnMeteor()
    {
        whichBorder = Random.Range(1, 4);

        if(whichBorder == 1) //Left
        {
            minX = -16.9f;
            maxX = -16.9f;
            minY = -8.9f;
            maxY = -8.9f;
        }
        else if(whichBorder == 2) //Top
        {
            minX = -16.9f;
            maxX = 16.9f;
            minY = 8.9f;
            maxY = 8.9f;
        }
        else if (whichBorder == 3) //Right
        {
            minX = 16.9f;
            maxX = 16.9f;
            minY = -8.9f;
            maxY = 8.9f;

        }
        else if (whichBorder == 4) //Bottom
        {
            minX = -16.9f;
            maxX = 16.9f;
            minY = -8.9f;
            maxY = -8.9f;
        }


        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(posX, posY);
        Instantiate(asteroid, spawnPosition, Quaternion.identity);

    }
}

