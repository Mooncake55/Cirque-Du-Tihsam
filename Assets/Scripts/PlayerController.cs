using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float actualSpeed;
    public float moveSpeed = 1.2f;
    public float runSpeed = 1.8f;
    public float jumpForce = 1.5f;
    [SerializeField]
    private float _moveInput;
    [SerializeField]
    private float _smoothMovement;
    Vector2 _lastMove = Vector2.zero;

    private Rigidbody2D _rb;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    
    private bool _onGround;
    private bool _jump;
    private float _jumpVelocity;
    public LayerMask ground;
    [SerializeField]
    private float _longRaycast = 0.1f; 
    [SerializeField]
    private float _originRaycast2 = 0.05f;
    [SerializeField]
    private float _originRaycast3 = 0.05f;

    private string _currentState;
    const string PLAYER_JUMP = "JumpClown";
    const string PLAYER_WALK = "WalkClown";
    const string PLAYER_IDLE = "IdleClown";
    const string PLAYER_FALL = "FallClown";

    private CinemachineVirtualCamera _virtualCamera;
    [SerializeField]
    private float _lookAheadTime;

    //public PauseMenu pauseMenu;
    //private bool _pause;

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
        //if (Input.GetKeyDown(KeyCode.Escape) && _pause == false)
        //{
        //    pauseMenu.Pause();
        //    _pause = true;
        //} else if(Input.GetKeyDown(KeyCode.H) && _pause == true)
        //{
        //    pauseMenu.Resume();
        //    _pause = false;
        //}
    }
    void FixedUpdate()
    {
        Movement();
        Flip();
        Animations();
    }
    public void Movement()
    {
        _moveInput = Input.GetAxis("Horizontal");

       // if(_lastXMovementInput > Mathf.Abs(moveInput))
        //{
          //  moveInput = 0;
        //}
        Vector2 objetiveVelocity = new Vector2(_moveInput * actualSpeed, _rb.velocity.y);
        _rb.velocity = Vector2.SmoothDamp(_rb.velocity, objetiveVelocity, ref _lastMove, _smoothMovement);

        _jumpVelocity = _rb.velocity.y;
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
        RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - _originRaycast2, transform.position.y), Vector2.down, _longRaycast, ground);
        RaycastHit2D hit3 = Physics2D.Raycast(new Vector2(transform.position.x - _originRaycast3, transform.position.y), Vector2.down, _longRaycast, ground);
        if (hit.collider != null || hit2.collider != null || hit3.collider != null)
        {
            _onGround = true;
        }
        else { _onGround = false; }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * _longRaycast);
        Gizmos.DrawLine(new Vector3(transform.position.x - _originRaycast2, transform.position.y, 0), new Vector3(transform.position.x - _originRaycast2, transform.position.y, 0) + Vector3.down * _longRaycast);
        Gizmos.DrawLine(new Vector3(transform.position.x - _originRaycast3, transform.position.y, 0), new Vector3(transform.position.x - _originRaycast3, transform.position.y, 0) + Vector3.down * _longRaycast);

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
            if (_moveInput > 0.1f || _moveInput < - 0.1f)
            {
                ChangeAnimationState(PLAYER_WALK);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }
        if (_onGround == false)
        {
            if (_jump == true && _jumpVelocity > 0)
            {
                ChangeAnimationState(PLAYER_JUMP);
                _jump = false;
                
            }
            if (_jumpVelocity < 0)
            {
                ChangeAnimationState(PLAYER_FALL);
            }
        }
    }
}

