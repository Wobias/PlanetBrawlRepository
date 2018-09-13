using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float maxZoom;
    public float minZoom;

    private Transform[] players = new Transform[4];
    private Transform camTrans;
    private Camera cam;
    private Vector2 target;

    private Vector2 minBounds;
    private Vector2 maxBounds;


    void Start()
    {
        camTrans = transform;
        cam = Camera.main;
        target = camTrans.position;

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        players = new Transform[playerObjects.Length];

        for (int i = 0; i < playerObjects.Length; i++)
        {
            players[i] = playerObjects[i].transform;
        }
    }

    void FixedUpdate()
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

        target = minBounds + (maxBounds - minBounds) / 2;
        
        camTrans.position = new Vector3(target.x, target.y, camTrans.position.z);

        cam.orthographicSize = Mathf.Clamp((maxBounds - minBounds).magnitude, maxZoom, minZoom);
    }
}
