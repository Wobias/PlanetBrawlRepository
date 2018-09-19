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
        ItemController itemController = other.GetComponent<ItemController>();

        if (itemController == null)
            return;

        GameObject item = Instantiate(itemPrefab, other.transform.position, Quaternion.identity, other.transform);

        if (itemType == ItemType.weapon)
        {
            itemController.WeaponPickUp(item);
        }
        else
        {
            itemController.ItemPickup(item);
        }

        Destroy(gameObject);
    }
}
