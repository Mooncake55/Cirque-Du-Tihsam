using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float actualSpeed;
    private float moveSpeed = 1.2f;
    private float runSpeed = 1.8f;
    public float jumpForce = 1.5f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        actualSpeed = moveSpeed;
    }

    // Update is called once per frame

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            
            actualSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            actualSpeed = moveSpeed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
    }
    void FixedUpdate()
    {
    Movement();
    Animations();
    Flip();
    }
    void Movement()
    {
        float moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveInput * actualSpeed, _rb.velocity.y);
    }
    void Animations()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0)
        {
            _animator.SetFloat("Speed", 1);
        }
        else
        {
            _animator.SetFloat("Speed", 0);
        }
    }
    void Flip()
    {
        //Gira el Sprite del personaje hacia apriete el jugador(izquierda o derecha)
        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
