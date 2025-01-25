using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice_bullet_script : MonoBehaviour
{
    private PlayerMovement playermovement;
    private PlayerAttack playerattack;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
           
            playermovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerattack = collision.gameObject.GetComponent<PlayerAttack>();
            disabling_the_script();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
        
            playermovement = other.gameObject.GetComponent<PlayerMovement>();
            playerattack = other.gameObject.GetComponent<PlayerAttack>();
            disabling_the_script();
        }
    }


   public void disabling_the_script()
    {
        playermovement.enabled = false;
        playerattack.enabled = false;
    }

}
