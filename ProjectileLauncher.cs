using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;

    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;
        if(transform.localScale.x < 0)
            // Flip the projectile's facing direction and movement based on the direction the character is facing
        {
            projectile.transform.localScale = new Vector3(
                origScale.x * -1,
                origScale.y,
                origScale.z
                );
        }
        if(transform.localScale.x > 0)
        {
            projectile.transform.localScale = new Vector3(
                origScale.x * 1,
                origScale.y,
                origScale.z
                );
        }

        
        
    }
}
