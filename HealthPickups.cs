using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickups : MonoBehaviour
{


    public int healthRestore = 20;
    public Vector3 spinRotationSpeed = new Vector3(0, 100, 0);

    private AudioSource pickupSource;



    public AudioClip pickupSound; // Assign this in the Inspector


    private void Start()
    {
        pickupSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        transform.eulerAngles += spinRotationSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damageable damageable = collision.GetComponent<Damageable>();// Make sure your player has the "Player" tag


        if (damageable && damageable.Health < damageable.MaxHealth)
        {
            bool wasHealed = damageable.Heal(healthRestore);

            if (wasHealed)
            {
                // Play sound
                if (pickupSource)
                
                    AudioSource.PlayClipAtPoint(pickupSound, transform.position, pickupSource.volume);
                

                Destroy(gameObject); // Destroy after sound plays
            }



        }
    }

}

