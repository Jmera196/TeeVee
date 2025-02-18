using Unity.VisualScripting;
using UnityEngine;

public class TikiHutProjectileLauncher : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject projectilePrefab;


    public void FireProjectile()
    {
        GameObject projectile = Instantiate(projectilePrefab, launchPoint.position, projectilePrefab.transform.rotation);
        Vector3 origScale = projectile.transform.localScale;

        // Flip the projectile's facing direction and movement based on the direction the character is facing
      
        

            projectile.transform.localScale = new Vector3(
            origScale.x * transform.localScale.x < 0 ? 1 : -1,
            origScale.y * transform.localScale.y,
            origScale.z * transform.localScale.z);
        
       
            
        
    }
    
}

                

           
    



