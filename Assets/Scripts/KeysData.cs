using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysData : MonoBehaviour
{
    private PlayerInventory _inventory;
    public string key_name;

    private void OnTriggerEnter2D(Collider2D col)
    {
        _inventory = col.gameObject.GetComponent<PlayerInventory>();

        if (_inventory == null) 
        {
            Debug.LogError("INVENTORY NULL");
            return;
        }
        if (col.CompareTag("Player"))
        {
            _inventory.Key_Collect(key_name);
            Destroy(this.gameObject);
        }
    }
}
