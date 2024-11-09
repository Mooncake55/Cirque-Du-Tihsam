using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloon : MonoBehaviour
{
    private PlayerInventory _inventory;
    private GameObject _bloon;
    private MovingPlataform _movingPlataform;
    public Transform posA, posB;
    public float speed;
    Vector2 targetPos;

    private Collider2D _collider;
    private SpriteRenderer _spriteRenderer;
    private GameObject _light;
    private void Start()
    {
        _bloon = gameObject;
        targetPos = posB.position;

        _light = transform.GetChild(0).gameObject;
        _collider = GetComponent<Collider2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void FixedUpdate()
    {
        MoveTo();
    }

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
            _collider.enabled = false;
            _spriteRenderer.enabled = false;
            Destroy(_light);
        }
    }
    public void MoveTo()
    {
        if (Vector2.Distance(transform.position, posA.position) < .1f) targetPos = posB.position;
        if (Vector2.Distance(transform.position, posB.position) < .1f) targetPos = posA.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPos, speed);
    }
}
