using UnityEngine;

public class CharacterFlip : MonoBehaviour
{
    public Transform player; // Assign the player transform in the Inspector
    private bool facingRight = true;

    void Update()
    {
        if (player != null)
        {
            FlipTowardsPlayer();
        }
    }

    void FlipTowardsPlayer()
    {
        float direction = player.position.x - transform.position.x;
        if ((direction < 0 && !facingRight) || (direction > 0 && facingRight))
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        FlipChildColliders();
    }

    void FlipChildColliders()
    {
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            Vector2 offset = col.offset;
            offset.x *= -1; // Flip the offset
            col.offset = offset;
        }
    }
}
