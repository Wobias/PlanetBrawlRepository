using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float followSpeed;
    public float zoomSpeed;
    public float maxZoom;
    public float minZoom;

    private List<Transform> players = new List<Transform>();
    //private PerfectPixelWithZoom pixelPerfectZoom;
    private Camera cam;
    private Transform camTrans;
    private Vector2 target;

    private Vector2 minBounds;
    private Vector2 maxBounds;


    void Start()
    {
        //pixelPerfectZoom = GetComponent<PerfectPixelWithZoom>();
        camTrans = transform;
        cam = Camera.main;
        target = camTrans.position;

        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < playerObjects.Length; i++)
        {
            players.Add(playerObjects[i].transform);
        }
    }

    void FixedUpdate()
    {
        if (players.Count < 1)
            return;

        if (players[0] != null)
        {
            minBounds = players[0].position;
            maxBounds = players[0].position;
        }

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i] != null)
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
            else
            {
                players.RemoveAt(i);
            }
        }

        target = minBounds + (maxBounds - minBounds) / 2;
        
        camTrans.position = Vector3.Lerp(camTrans.position, new Vector3(target.x, target.y, camTrans.position.z), followSpeed);

        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, Mathf.Clamp((maxBounds - minBounds).magnitude, maxZoom, minZoom), zoomSpeed);
        //pixelPerfectZoom.SetZoom((maxBounds - minBounds).magnitude / maxZoom * 2);
    }
}
