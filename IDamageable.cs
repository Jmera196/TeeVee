using UnityEngine;

public interface IDamageable
{
    public bool Targetable { set; get; }
    public float Health { set; get; }
    public void OnHit(float damage, Vector2 knockback);
    public void OnHit(float damage);
    public void DestroySelf();

}

    
