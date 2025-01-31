using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPlane : MonoBehaviour
{
    [SerializeField] private DynamicCamera dynamicCamera;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Hide the player 
            collision.gameObject.GetComponent<HealthManager>().damageHealth(100);
        
        }
        else if (collision.gameObject.CompareTag("AmmoCollectible"))
        {
            Destroy(collision.gameObject);
        }

    }
}
