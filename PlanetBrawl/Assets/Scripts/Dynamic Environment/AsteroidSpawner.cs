using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{

    public float spawnTimeout = 5;
    private float spawnTimer = 5;
    public float itemChance = 20;
    //public int spawnMin = 0;
    //public int spawnMax = 1000;
    public GameObject asteroid;
    public GameObject itemAsteroid;

    public Transform bottomLeftSpawn;
    public Transform topRightSpawn;
    int whichBorder;

    public string asteroidSpawnSound = "asteroidSpawn";


    private void Start()
    {
        spawnTimer = spawnTimeout;
    }

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
        if (spawnTimer <= 0)
        {
            spawnTimer = spawnTimeout;
            SpawnMeteor();
        }
    }

    void SpawnMeteor()
    {
        whichBorder = Random.Range(0, 4);

        Vector2 spawnPos = Vector2.zero;
        Vector2 dir = Vector2.zero;

        switch (whichBorder)
        {
            case 0:
                spawnPos.x = bottomLeftSpawn.position.x;
                spawnPos.y = Random.Range(bottomLeftSpawn.position.y, topRightSpawn.position.y);
                dir.x = 1;
                dir.y = Random.Range(-1f, 1f);
                break;
            case 1:
                spawnPos.x = topRightSpawn.position.x;
                spawnPos.y = Random.Range(bottomLeftSpawn.position.y, topRightSpawn.position.y);
                dir.x = -1;
                dir.y = Random.Range(-1f, 1f);
                break;
            case 2:
                spawnPos.y = bottomLeftSpawn.position.y;
                spawnPos.x = Random.Range(bottomLeftSpawn.position.x, topRightSpawn.position.x);
                dir.x = Random.Range(-1f, 1f);
                dir.y = 1;
                break;
            case 3:
                spawnPos.y = topRightSpawn.position.y;
                spawnPos.x = Random.Range(bottomLeftSpawn.position.x, topRightSpawn.position.x);
                dir.x = Random.Range(-1f, 1f);
                dir.y = -1;
                break;
        }

        bool isItem = Random.Range(0f, 100f) <= itemChance;

        AsteroidController newAsteroid;
        
        if (!isItem)
            newAsteroid = Instantiate(asteroid, spawnPos, Quaternion.identity).GetComponent<AsteroidController>();
        else
            newAsteroid = Instantiate(itemAsteroid, spawnPos, Quaternion.identity).GetComponent<AsteroidController>();

        newAsteroid.direction = dir;

        AudioManager1.instance.Play(asteroidSpawnSound);

    }
}

