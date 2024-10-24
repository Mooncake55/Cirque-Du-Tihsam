using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float _moveSpeed = 2f;
    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _moveSpeed = 5;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            _moveSpeed = 2f;
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
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveInput = new Vector3(horizontalInput, verticalInput, 0);

        Vector3 nextPosition = transform.position + moveInput * Time.deltaTime * _moveSpeed;
        _rb.MovePosition(nextPosition);
    }
    void Animations()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        if (horizontal != 0 || vertical != 0)
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
}
