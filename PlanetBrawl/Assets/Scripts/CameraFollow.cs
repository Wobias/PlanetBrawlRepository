using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    public float zoomSpeed;

    private Transform[] players = new Transform[4];
    private Transform cam;
    private Vector3 target;

    private Vector3 minBounds;
    private Vector3 maxBounds;


    void Start()
    {
        cam = transform;
        target = cam.position;

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        players = new Transform[playerObjects.Length];

        for (int i = 0; i < playerObjects.Length; i++)
        {
            players[i] = playerObjects[i].transform;
        }
    }

    void FixedUpdate()
    {
        if (players != null)
        {
            minBounds = players[0].position;
            maxBounds = players[0].position;

            for (int i = 1; i < players.Length; i++)
            {
                if (players[i].position.x < minBounds.x)
                {
                    minBounds.x = players[i].position.x;
                }
                else if (players[i].position.x > maxBounds.x)
                {
                    maxBounds.x = players[i].position.x;
                }

                if (players[i].position.y < minBounds.y)
                {
                    minBounds.y = players[i].position.y;
                }
                else if (players[i].position.y > maxBounds.y)
                {
                    maxBounds.y = players[i].position.y;
                }
            }

            target = minBounds + (maxBounds - minBounds) / 2 + new Vector3(0,0,cam.position.z);
        }
        
        cam.position = target;
    }
}
