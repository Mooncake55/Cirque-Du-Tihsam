using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysData : MonoBehaviour
{
    private PlayerInventory _inventory;
    public string key_name;
    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
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
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
        }
    }
}
