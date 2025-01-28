using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shock_bullet : MonoBehaviour
{
    private HealthManager healthmanager;
    private PlayerAttack playerattack;


    public BubbleGums bubblegums;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthmanager = collision.gameObject.GetComponent<HealthManager>();
            playerattack = collision.gameObject.GetComponent<PlayerAttack>();

            healthmanager.damageHealth(bubblegums.baseDamage);

            playerattack.can_shoot = false;
            playerattack.isShocked = true;
            playerattack.IsShocked();

            playerattack.Invoke("ResetMat", 5f);
            playerattack.Invoke("reset_shooting_function", 5f);

           

            Destroy(this.gameObject);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        bubble_shotter_Bar = other.gameObject.GetComponent<Bubble_shotter_bar>();
    //        bubble_shotter_Bar.Reset_call_another_script(); 
    //    }
    //}
}
