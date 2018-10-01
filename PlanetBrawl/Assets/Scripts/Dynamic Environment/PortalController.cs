using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    public GameObject otherPortal;
    public bool isPortalReady = true;
    public float coolDown = 3f;


    void Update()
    {
        if (isPortalReady == false)
        {
            coolDown -= Time.deltaTime;
            if (coolDown <= 0)
            {
                isPortalReady = true;
                coolDown = 3f;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && isPortalReady == true)
        {
            other.gameObject.transform.position = new Vector2(otherPortal.transform.position.x, otherPortal.transform.position.y);
            isPortalReady = false;
            otherPortal.GetComponent<PortalController>().isPortalReady = false;
        }
    }
}
