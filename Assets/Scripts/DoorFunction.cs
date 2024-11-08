using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    public KeysData _keysData;
    private PlayerInventory _inventory;
    private Collider2D _collider;
    public Collider2D colliderTrigger;
    private SpriteRenderer _spriteRenderer;
    [SerializeField]
    private Sprite _otherImage;
    private GameObject _light;

    private string _onInventoryKeyName;
    private string _correctKey;
    private bool _doorOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        _correctKey = _keysData.key_name;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light = transform.GetChild(0).gameObject;
    }
    private void Open_Door() 
    {
        _collider.enabled = false;
        _spriteRenderer.sprite = _otherImage;
        _doorOpened = true;
        transform.position = new Vector2(transform.position.x + 0.04f, transform.position.y);
        _light.transform.position = new Vector2(transform.position.x - 0.04f, transform.position.y);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (col.CompareTag("Player"))
        {
            _inventory = col.gameObject.GetComponent<PlayerInventory>();
            if (_inventory == null)
            {
                Debug.LogError("INVENTORY NULL");
                return;
            }

            foreach (string keys in _inventory.keysInventory)
            {
                if (keys == _correctKey)
                {
                    Debug.Log("You have the correct Key, the door is open");
                    Open_Door();
                }
                else
                {
                    Debug.Log("You dont have the correct key, you cannot open this door");
                }

            }
            if (_doorOpened == true)
            {
                _inventory.keysInventory.Remove(_correctKey);
            }
        }
    }

}
