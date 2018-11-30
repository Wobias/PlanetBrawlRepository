using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public Transform[] spawnPoints;

    public GameObject portalOne;
    public GameObject portalTwo;

    public float respawnTime = 5f;
    public float activeTime = 10f;

    private float portalTimer;
    private float portalResetTimer;

    public bool portalIsOpen = false;


    private void Start()
    {
        portalOne.SetActive(false);
        portalTwo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (portalTimer > 0)
            portalTimer -= Time.deltaTime;

        if (portalTimer <= 0f && portalIsOpen == false)
        {
            SetPortal();
            portalResetTimer = activeTime;
        }

        if (portalIsOpen == true)
        {
            portalResetTimer -= Time.deltaTime;

            if (portalResetTimer <= 0f)
            {
                DisablePortals();
            }
        }
    }

    public void DisablePortals()
    {
        portalIsOpen = false;
        portalOne.SetActive(false);
        portalTwo.SetActive(false);
        portalTimer = respawnTime;
    }

    private void SetPortal()
    {
        Vector2 portal1Pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        Vector2 portal2Pos;

        do
        {
            portal2Pos = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
        }
        while (portal2Pos == portal1Pos);

        portalOne.transform.position = portal1Pos;
        portalTwo.transform.position = portal2Pos;

        portalOne.SetActive(true);
        portalTwo.SetActive(true);

        portalIsOpen = true;
    }
}
