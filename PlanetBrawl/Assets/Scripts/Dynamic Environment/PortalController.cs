using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public PortalController[] otherPortals;
    private PortalSpawner spawner;


    private void Start()
    {
        spawner = FindObjectOfType<PortalSpawner>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        int portalSelection = Random.Range(0, otherPortals.Length - 1);

        other.transform.root.position = otherPortals[portalSelection].transform.position;

        spawner.DisablePortals();
    }
}
