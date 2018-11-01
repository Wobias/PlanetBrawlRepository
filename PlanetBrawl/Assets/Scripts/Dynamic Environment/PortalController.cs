using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject otherPortal;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        other.transform.root.position = otherPortal.transform.position;

        PortalSpawner.portalSpawner.portalIsOpen = false;
        PortalSpawner.portalSpawner.portalTimer = Random.Range(2f, 8f);

        otherPortal.SetActive(false);
        gameObject.SetActive(false);
    }

}
