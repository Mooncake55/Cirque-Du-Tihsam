using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEngine.UIElements;
using System;
using TMPro;
using Unity.VisualScripting;

public class EnemyAI : MonoBehaviour
{
    /*[Header("Pathfinding")]
    public Transform target;
    public float activeDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 0.8f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequitement = 0.8f;
    public float jumpModifier = 1.2f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnable = true;
    public bool jumpEnable = true;
    public bool directionLookEnabled = true;

    private Path path;
    private int _currentWaypoint = 0;
    bool _isGrounded = false;
    public Seeker _seeker;
    public Rigidbody2D _rb;
    public Collider2D col;
    public LayerMask groundLayer;

    private void Start()
    {
        _seeker = GetComponent<Seeker>();
        _rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnable)
        {
            PathFollow();
        }

    }
    private void UpdatePath()
    {
        if (TargetInDistance() && followEnable && _seeker.IsDone())
        {
            _seeker.StartPath(_rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }
        //Reached end of the path
        if(_currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }
        //Calculate collidings
        //Vector3 startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
        //_isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        RaycastHit2D isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        //Direction
        Vector2 direction = ((Vector2)path.vectorPath[_currentWaypoint] - _rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (_isGrounded && jumpEnable) 
        {
            if (direction.y < jumpNodeHeightRequitement) 
            {
                _rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }
        
        //MOVE
        _rb.AddForce(force);

        //Next waypoint
        float distance = Vector2.Distance(_rb.position, path.vectorPath[_currentWaypoint]);
        if (distance < nextWaypointDistance) 
        {
            _currentWaypoint++; 
        }

        //Flip sprite
        if (directionLookEnabled) 
        {
            if(_rb.velocity.x > 0.5f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if(_rb.velocity.x < -0.05f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activeDistance; 
    }
    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            _currentWaypoint = 0;
        }
    }*/

    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200f, jumpForce = 100f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true, isJumping, isInAir;
    public bool directionLookEnabled = true;

    [SerializeField] Vector3 startOffset;

    private Path path;
    private int currentWaypoint = 0;
    [SerializeField] public RaycastHit2D isGrounded;
    Seeker seeker;
    Rigidbody2D rb;
    private bool isOnCoolDown;
    public CapsuleCollider2D col;
    public LayerMask groundLayer;
    [SerializeField]
    private float _actualspeed;
    AILerp lerp;


    public void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        isJumping = false;
        isInAir = false;
        isOnCoolDown = false;
        col = GetComponent<CapsuleCollider2D>();
        col.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        lerp = GetComponent<AILerp>();
        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);
    }

    private void FixedUpdate()
    {
        if (TargetInDistance() && followEnabled)
        {
            if (CalculateDistance() > 1.2f) 
            {
                _actualspeed = speed * 2f;
                lerp.speed = 1.5f;
            }
            else if (CalculateDistance() < 1)
            {
                _actualspeed = speed;
                lerp.speed = 1;
            }
           
            PathFollow();
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    private void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything
        startOffset = transform.position - new Vector3(0f, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset, transform.position.z);
        isGrounded = Physics2D.Raycast(startOffset, -Vector3.up, 0.05f);
        //RaycastHit2D isGrounded = Physics2D.BoxCast(col.bounds.center, col.bounds.size, 0, Vector2.down, 0.1f, groundLayer);

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * _actualspeed;

        // Jump
        if (jumpEnabled && isGrounded && !isInAir && !isOnCoolDown)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                if (isInAir) return;
                isJumping = true;
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                StartCoroutine(JumpCoolDown());

            }
        }
        if (isGrounded)
        {
            isJumping = false;
            isInAir = false;
        }
        else
        {
            isInAir = true;
        }

        // Movement
        rb.velocity = new Vector2(force.x, rb.velocity.y);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x > 0.0f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x < -0.0f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    private bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    private void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    IEnumerator JumpCoolDown()
    {
        isOnCoolDown = true;
        yield return new WaitForSeconds(0.1f);
        isOnCoolDown = false;
    }

    public float CalculateDistance()
    {
        float _distance;
        _distance = Vector2.Distance(transform.position, target.transform.position);
        return Mathf.Abs(_distance);
    }
}
