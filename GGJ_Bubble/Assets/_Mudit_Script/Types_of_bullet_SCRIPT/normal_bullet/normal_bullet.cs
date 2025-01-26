using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class normal_bullet : MonoBehaviour
{
    private HealthManager healthmanager;
    public BubbleGums bubblegums;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthmanager = collision.gameObject.GetComponent<HealthManager>();
            healthmanager.damageHealth(bubblegums.baseDamage);
            Destroy(this.gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        healthmanager = other.gameObject.GetComponent<HealthManager>();
    //        normal_gum_attack();
    //    }
        
    //}

    
}
