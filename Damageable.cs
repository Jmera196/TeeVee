using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Events;
public class Damageable : MonoBehaviour
{
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;
    public UnityEvent<int, int> healthChanged;
    Animator animator;   
    float _currentHealth;
    HitStop _freezer;
    [SerializeField]
    private int _maxHealth = 100;
    public int MaxHealth
    {
        get { return _maxHealth; }
        set
            {
                _maxHealth = value;
            }
    }

    
    [SerializeField]
    private int _health = 100;
    public int Health
    {
        get { return _health; }
        set
        {
            // If health drops to or below 0, character is no longer alive
            _health = value;
            healthChanged.Invoke(_health, _maxHealth);
            if (_health <= 0)
            {

                IsAlive = false;
            }
        }
    }

    [SerializeField]
    private bool _isAlive = true;

    [SerializeField]
    private bool isInvincible = false;
    
    
    public float invincibilityTime = 0.25f;
    private float timeSinceHit = 0;

    public bool IsAlive
    {
        get { return _isAlive; }
        set
        {
            _isAlive = value;
            animator.SetBool(AnimationStrings.isAlive, value);
            Debug.Log("IsAlive set" + value);

            if(value == false)
            {
                //Notify other subscribed components that the character has died
                damageableDeath.Invoke();
            }
        }
    }
    // The velocity should not be changed while this is true but needs to be respexted by other physics components like
    // the player controller
    public bool LockVelocity
    {
        get
        {
            return animator.GetBool(AnimationStrings.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationStrings.lockVelocity, value);
        }
    }

    private void Start()
    {
        GameObject mgr = GameObject.FindWithTag("Manager");
        if (mgr)
        {
            _freezer = mgr.GetComponent<HitStop>();
            
        }
        _currentHealth = MaxHealth;
    }


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    

    private void Update()
    {
        if (isInvincible)
        {
            if(timeSinceHit > invincibilityTime)
            {
                //Remove Invincibility
                isInvincible = false;
                timeSinceHit = 0;
            }

            timeSinceHit += Time.deltaTime;
        }
        
    }

    public bool hurt(int damage, Vector2 knockback)
    {
        _currentHealth -= damage;
        if (IsAlive && !isInvincible)
        {
            _freezer.Freeze();
            Health -= damage;
            isInvincible = true;
            //Notify other subscribed conmponents that the damageable was hit to handle the knockback and such
            animator.SetTrigger(AnimationStrings.hurtTrigger);
            LockVelocity = true;
            damageableHit?.Invoke(damage, knockback);
            CharacterEvents.characterDamaged.Invoke(gameObject, damage);

            return true;
        }

        //Unable to be hurt
        return false;
    }

    // Returns whether the character was healed or not
    public bool Heal(int healthRestore)
    {
        if (IsAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, healthRestore);
            Health += actualHeal;
            CharacterEvents.characterHealed(gameObject, actualHeal);
            return true;
        }

        return false;
    }
}





