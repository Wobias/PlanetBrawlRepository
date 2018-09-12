using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicEnvironment : MonoBehaviour {

    public int spawnMin = 0;
    public int spawnMax = 300;
    public GameObject prefab;
    public float minX = -3.7f;
    public float maxX = 3.7f;
    public float minY = -7.4f;
    public float maxY = 7.4f;
    public float rotateSpeed = 50f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float sum = Random.Range(spawnMin, spawnMax);
        Debug.Log(sum);
        if (sum == 50)
        {
            Invoke("SpawnMeteor", 1);
        }
        prefab.transform.Rotate(Vector3.back * Time.deltaTime * rotateSpeed);

    }
    void SpawnMeteor()
    {
        float posX = Random.Range(minX, maxX);
        float posY = Random.Range(minY, maxY);
        Vector2 spawnPosition = new Vector2(posX, posY);
        Instantiate(prefab, spawnPosition, Quaternion.identity);

    }
}

