using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bazuka_bullet : MonoBehaviour
{
    public GameObject[] Playerss;
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
