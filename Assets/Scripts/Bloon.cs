using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    private PlayerInventory _inventory;

    public void OnTriggerEnter2D(Collider2D col)
    {
        _inventory = col.gameObject.GetComponent<PlayerInventory>();
        if (_inventory == null)
        {
            Debug.LogError("INVENTORY NULL");
            return;
        }
        if (col.CompareTag("Player"))
        {
            _inventory.bloonInventory++;
            Destroy(this.gameObject);
        }
    }
}
