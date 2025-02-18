using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SwordHitBox : MonoBehaviour
{
    public float swordDamage = 3f;
    public float knockbackForce = 15f;
    public Collider2D swordCollider;
    public Vector3 faceRight = new Vector3(0, 0, 0);
    public Vector3 faceLeft = new Vector3(-0.35f, -0.01f, 0);

     void Start()
    {
        if (swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageableObject = collider.GetComponent<IDamageable>();

        if (damageableObject != null)
        {

            //calculate direction between character and slime
            Vector3 parentPosition = transform.parent.position;


            //offset for Collision detection changes the direction where the force comes from (close to the player)
            Vector2 direction = (collider.transform.position - parentPosition).normalized;


            // knockback is the direction of swordCollider towards collider
            Vector2 knockback = direction * knockbackForce;

            //after making sure the collider has a script that implements IDamageable, we can run the OnHit and pass our Vector2 force
            damageableObject.OnHit(swordDamage, knockback);

        }
    }

    

    //Keep Collider offset to 0 so that a flip to the left and a flip to the right have the same distance from the transform
    void IsFacingRight(bool isFacingRight)
    {
        if (isFacingRight)
        {
            gameObject.transform.localPosition = faceRight;
        }
        else
        {
            gameObject.transform.localPosition = faceLeft;
        }
    }



}
