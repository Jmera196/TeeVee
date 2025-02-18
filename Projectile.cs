using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Projectile : MonoBehaviour
{
    public int damage = 5;
    public Vector2 moveSpeed = new Vector2(10f,0);
    public Vector2 knockback = new Vector2(0, 0);
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb.linearVelocity = new Vector2(moveSpeed.x * transform.localScale.x, moveSpeed.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();

        if(damageable != null)
        {
            // If Parent is facing the left by localScale, our knockback x flips its value to face the left as well
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            //  hit the target
            bool gotHurt = damageable.hurt(damage, deliveredKnockback);
            if (gotHurt)
                Debug.Log(collision.name + "hurt for " + damage);
                Destroy(gameObject);
        }
    }
}
