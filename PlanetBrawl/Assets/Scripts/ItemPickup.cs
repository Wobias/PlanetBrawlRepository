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
        WeaponController weaponController = other.GetComponent<WeaponController>();

        if (weaponController == null)
            return;

        GameObject item = Instantiate(itemPrefab, other.transform.position, Quaternion.identity, other.transform);

        if (itemType == ItemType.weapon)
        {
            weaponController.WeaponPickUp(item);
        }

        Destroy(gameObject);
    }
}
