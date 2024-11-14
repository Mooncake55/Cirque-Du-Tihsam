using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeysData : MonoBehaviour
{
    private PlayerInventory _inventory;
    public string key_name;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
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
            _spriteRenderer.enabled = false;
            _collider.enabled = false;
        }
    }
}
