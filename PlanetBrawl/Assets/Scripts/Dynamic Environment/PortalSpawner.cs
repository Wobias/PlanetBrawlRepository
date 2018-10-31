using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalSpawner : MonoBehaviour
{
    public static PortalSpawner portalSpawner;

    public GameObject portalOne;
    public GameObject portalTwo;

    public float portalTimer = 5f;
    private float portalResetTimer = 10f;

    public bool portalIsOpen = false;

    private Vector2 portalOnePosition;

    private void Start()
    {
        portalSpawner = this;

        portalOne.SetActive(false);
        portalTwo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        portalTimer -= Time.deltaTime;

        if (portalTimer <= 0f && portalIsOpen == false)
        {
            SetPortal();
            portalTimer = Random.Range(3f, 10f);
        }

        if (portalIsOpen == true)
        {
            portalResetTimer -= Time.deltaTime;

            if (portalResetTimer <= 0f)
            {
                portalIsOpen = false;
                portalResetTimer = Random.Range(5f, 15f);
            }
        }
    }

    private void SetPortal()
    {
        portalOnePosition.x = Random.Range(-18f, 19f);
        portalOnePosition.y = Random.Range(-8f, 9f);

        portalOne.transform.position = portalOnePosition;
        portalTwo.transform.position = portalOnePosition * -1;

        portalOne.SetActive(true);
        portalTwo.SetActive(true);

        portalIsOpen = true;
    }
}
