using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    public GameObject item;
	
    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject otherObject = coll.gameObject;

        if (otherObject.tag == "Asteroid" /*"Tag"*/)
        {
            float chance = Random.Range(0f, 10f);
            if (chance <= 5)
            {
                Instantiate(/* Item */item, otherObject.transform.position, Quaternion.identity);
            }
        }
            
    }
}
