using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazuka_bullet : MonoBehaviour
{
    public GameObject[] Playerss;
    public BubbleGums bubblegums;
    public GameObject explosionPrefab; // Assign your explosion prefab here abhisheks change
    private Bubble_shotter_bar powerBarlevel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
           if (bubblegums.areaDamage)
            {
                ApplyAreaDamage();
            }

            PlayExplosion(); //explosion effect;
            Destroy(this.gameObject);
        }
    }


    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject)
    //    {
    //        if (bubblegums.areaDamage)
    //        {
    //            ApplyAreaDamage();
    //        }
    //    }
    //}

    private void PlayExplosion()
    {
        if (explosionPrefab != null)
        {
            // Instantiate the explosion prefab at the current position and rotation
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned!");
        }
    }

    private void ApplyAreaDamage()
    {
        // Find all colliders within the defined area radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, bubblegums.areaRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the object has the "Player" tag
            if (hitCollider.CompareTag("Player"))
            {
                // Do something with the player object, e.g., damage or apply an effect
                Debug.Log($"Player in area: {hitCollider.gameObject.name}");

                // Example: Apply damage or effect to the player
                HealthManager playerHealth = hitCollider.GetComponent<HealthManager>();
                powerBarlevel = hitCollider.gameObject.GetComponent<Bubble_shotter_bar>();
                if (playerHealth != null)
                {
                    playerHealth.damageHealth(bubblegums.baseDamage * powerBarlevel.powerLevel); // Apply 10 damage as an example
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the area radius in the editor
        if (bubblegums != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bubblegums.areaRadius);
        }
    }


}
/*
{
    public GameObject explosionPrefab; // Assign your explosion prefab here
    public BubbleGums bubblegums;

    private Bubble_shotter_bar powerBarlevel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject)
        {
            if (bubblegums.areaDamage)
            {
                ApplyAreaDamage();
            }

            // Play the explosion effect
            PlayExplosion();

            // Destroy the bullet
            Destroy(this.gameObject);
        }
    }

    private void ApplyAreaDamage()
    {
        // Find all colliders within the defined area radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, bubblegums.areaRadius);

        foreach (Collider hitCollider in hitColliders)
        {
            // Check if the object has the "Player" tag
            if (hitCollider.CompareTag("Player"))
            {
                // Example: Apply damage or effect to the player
                Debug.Log($"Player in area: {hitCollider.gameObject.name}");

                HealthManager playerHealth = hitCollider.GetComponent<HealthManager>();
                powerBarlevel = hitCollider.gameObject.GetComponent<Bubble_shotter_bar>();
                if (playerHealth != null)
                {
                    playerHealth.damageHealth(bubblegums.baseDamage * powerBarlevel.powerLevel);
                }
            }
        }
    }

    private void PlayExplosion()
    {
        if (explosionPrefab != null)
        {
            // Instantiate the explosion prefab at the current position and rotation
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning("Explosion prefab is not assigned!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the area radius in the editor
        if (bubblegums != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, bubblegums.areaRadius);
        }
    }
}

*/
