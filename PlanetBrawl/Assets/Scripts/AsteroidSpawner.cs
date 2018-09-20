using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{


    private float spawnTimer = 5;
    //public int spawnMin = 0;
    //public int spawnMax = 1000;
    public GameObject asteroid;

    public float minX = -3.7f;
    public float maxX = 3.7f;
    public float minY = -7.4f;
    public float maxY = 7.4f;


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

            spawnTimer = 5;
        }

        if (spawnTimer == 5)
        {
            SpawnMeteor();
        }




    }
    void SpawnMeteor()
    {
        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(posX, posY);
        Instantiate(asteroid, spawnPosition, Quaternion.identity);

    }
}

