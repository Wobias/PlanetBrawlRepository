using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public GameObject itemPrefab;

    public enum ItemType { weapon, powerUp };
    public ItemType itemType;


    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController itemController = other.transform.parent.GetComponent<PlayerController>();

        if (itemController == null)
            return;

        if (itemType == ItemType.weapon)
        {
            WeaponController weapon = Instantiate(itemPrefab, other.transform.position, other.transform.parent.Find("WeaponOrbit").rotation, other.transform.parent.Find("WeaponOrbit")).GetComponent<WeaponController>();
            itemController.WeaponPickUp(weapon);
        }
        else
        {
            GameObject item = Instantiate(itemPrefab, other.transform.position, Quaternion.identity, other.transform);
            itemController.ItemPickup(item);
        }

        Destroy(gameObject);
    }
}
