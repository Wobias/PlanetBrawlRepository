using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public float timeout;
    public PortalController[] otherPortals;

    
    void OnTriggerEnter2D(Collider2D other)
    {
        int portalSelection = Random.Range(0, otherPortals.Length - 1);

        other.transform.root.position = otherPortals[portalSelection].transform.position;

        //PortalSpawner.portalSpawner.portalIsOpen = false;
        //PortalSpawner.portalSpawner.portalTimer = Random.Range(2f, 8f);

        otherPortals[portalSelection].gameObject.SetActive(false);
        StartCoroutine(otherPortals[portalSelection].EnablePortal());
    }

    public IEnumerator EnablePortal()
    {
        yield return new WaitForSeconds(timeout);

        gameObject.SetActive(true);
    }
}
