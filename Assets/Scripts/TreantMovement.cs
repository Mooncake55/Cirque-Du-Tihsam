using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreantMovement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    private Vector2 _target;
    private Vector2 _position;
    private Vector2 previousPosition;

    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rb;
    private Light _light; 
    void Start()
    {
        _target = new Vector2();
        _position = gameObject.transform.position;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _light = GetComponent<Light>();
        previousPosition = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        
    }

    private void FixedUpdate()
    {
        Movement();
        
    }
    private void Movement() 
    {
        
        _target = player.transform.position;
        transform.position = Vector2.MoveTowards(transform.position, _target, speed * Time.deltaTime);    
    }
    void Flip()
    {
        Vector2 currentDirection = (transform.position - (Vector3)previousPosition).normalized;
        //Gira el Sprite
        if (currentDirection.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (currentDirection.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        previousPosition = transform.position;

    }

}
