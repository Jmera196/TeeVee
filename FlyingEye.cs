using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;
public class FlyingEye : MonoBehaviour
{
    Animator animator;
    Rigidbody2D rb;
    public Collider2D deathCollider;
    public DetectionZone biteDetectionZone;
    public float flightSpeed = 5f;
    private float waypointReachedDistance = 0.1f;
    public List<Transform> waypoints = new List<Transform>();
    public bool _hasTarget = false;
    Damageable damageable;
    Transform nextWaypoint;
    int waypointNum = 0;
    public bool HasTarget
    {
        get { return _hasTarget; }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationStrings.hasTarget, value);
        }
    }
    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();

    }

    private void Start()
    {
        nextWaypoint = waypoints[waypointNum];
    }

    private void OnEnable()
    {
        damageable.damageableDeath.AddListener(OnDeath);
    }

    // Update is called once per frame
    void Update()
    {
        HasTarget = biteDetectionZone.detectedColliders.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.IsAlive)
        {
            if (CanMove)
            {
                Flight();
            }
            else
            {
                rb.linearVelocity = Vector2.zero;
            }
        }    
    }

    private void Flight()
    {
        // Fly to the Waypoint
        Vector2 directionToWaypoint = (nextWaypoint.position - transform.position).normalized;

        // Check if we have reached the waypoint already
        float distance = Vector2.Distance(nextWaypoint.position, transform.position);

        rb.linearVelocity = directionToWaypoint * flightSpeed;
        UpdateDirection();

        // See if we need to switch waypoints
        if (distance <= waypointReachedDistance)
        {
            // swtich to the next waypoint
            waypointNum++;
            if (waypointNum >= waypoints.Count)
            {
                // Loop back to the original waypoint
                waypointNum = 0;
            }
            nextWaypoint = waypoints[waypointNum];
        }
    }
    private void UpdateDirection()
    {
        Vector3 locScale = transform.localScale;

        if (transform.localScale.x > 0)
        {
            //Facing the Right
            if (rb.linearVelocity.x < 0)
            {
                //Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
        else
        {
            //Facing the Left
            if (rb.linearVelocity.x > 0)
            {
                //Flip
                transform.localScale = new Vector3(-1 * locScale.x, locScale.y, locScale.z);
            }
        }
    }

    public void OnDeath()
    {
        {
            //Dead Flyer falls to ground
            rb.gravityScale = 1.5f;
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            deathCollider.enabled = true;
        }
    }

}










