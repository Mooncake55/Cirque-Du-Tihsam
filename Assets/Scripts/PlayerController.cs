using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float actualSpeed;
    private float moveSpeed = 1.2f;
    private float runSpeed = 1.8f;
    public float jumpForce = 1.5f;
    float moveInput;


    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private bool _onGround;
    private bool _jump;
    public LayerMask ground;
    [SerializeField]
    private float _longRaycast = 0.1f;


    private string _currentState;
    const string PLAYER_JUMP = "JumpClown";
    const string PLAYER_WALK = "walkClown";
    const string PLAYER_IDLE = "IdleClown";



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
        CalculateOnGround();

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            actualSpeed = runSpeed;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            actualSpeed = moveSpeed;
        }

        if (_onGround && Input.GetKey(KeyCode.Space))
        {
            Jump();
            _jump = true;
        }
    }
    void FixedUpdate()
    {
    Movement();
    Flip();
    Animations();
    }
    public void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");
        _rb.velocity = new Vector2(moveInput * actualSpeed, _rb.velocity.y);
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
    private void Jump()
    { 
        _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
    }
    private void CalculateOnGround()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _longRaycast, ground); 
        _onGround = hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _longRaycast);
    }
    public void ChangeAnimationState(string newState)
    {
        if (_currentState == newState) return;

        _animator.Play(newState);
        _currentState = newState;
    }
    private void Animations()
    {
        if (_onGround == true)
        {
            if (moveInput != 0)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if (_jump == true && _onGround == false)
        {
            ChangeAnimationState(PLAYER_JUMP);
            _jump = false;
        }
    }
}

