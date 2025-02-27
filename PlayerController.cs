using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]
public class PlayerController : MonoBehaviour
{
    
    public float walkSpeed = 5f;
    public float runSpeed = 20f;
    public float airRunSpeed = 3f;
    public float airWalkSpeed = 3f;
    public float jumpImpulse = 30f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    

    public float CurrentMoveSpeed
    {
        get
        {
            if (CanMove)
            {
                if (IsMoving && !touchingDirections.IsOnWall)

                {
                    if (touchingDirections.IsGrounded)
                    {
                        if (IsRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }

                    }
                    else
                    {
                        if (IsRunning && IsMoving)
                            // Air Move
                            return airRunSpeed;
                        else
                        {
                            return airWalkSpeed;
                        }

                    }
                }
                else
                {
                    //idle speed is 0
                    return 0;
                }
            }
            else
            {
                // Movement is Locked
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }


    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get
        {
            return _isFacingRight;
        }
        private set
        {
            //Flip only if valyue is new
            if (_isFacingRight != value)
            {
                // Flip the loval scale to make the player face the opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    public bool CanMove
    {
        get
        {
            return animator.GetBool(AnimationStrings.canMove);
        }
    }

    public bool IsAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();

    }


    private void FixedUpdate()
    {
        if (!damageable.LockVelocity)
            rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);
        animator.SetFloat(AnimationStrings.yVelocity, rb.linearVelocity.y);
     
    }

  

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        if (IsAlive)
        {
            IsMoving = moveInput != Vector2.zero;

            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }


    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            IsRunning = true;
        }
        else if (context.canceled)
        {
            IsRunning = false;
        }
    }
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //Face the Right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            //Face the Left
            IsFacingRight = false;
        }

    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO Check if alive as well
        if (context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
        }

    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttackTrigger);
        }
    }

    public void OnSpecialAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.specialAttackTrigger);
        }
    }

    public void OnHit(int damage, Vector2 knockback)
    {

        rb.linearVelocity = new Vector2(knockback.x, rb.linearVelocity.y + knockback.y);
    }


}

   

   
