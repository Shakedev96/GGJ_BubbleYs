using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice_bullet_script : MonoBehaviour
{
    [SerializeField] private PlayerMovement playermovement;
    [SerializeField] private PlayerAttack playerattack;

    private HealthManager healthmanager;
    public BubbleGums bubblegums;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            healthmanager = collision.gameObject.GetComponent<HealthManager>();

            playermovement = collision.gameObject.GetComponent<PlayerMovement>();
            playerattack = collision.gameObject.GetComponent<PlayerAttack>();

            playermovement.canMove = false;
            playermovement.Invoke("reseting_the_ismoving_bool_true", 3.2f);

            playerattack.can_shoot = false;
            playerattack.Invoke("reset_shooting_function", 3.2f);

            healthmanager.damageHealth( bubblegums.baseDamage);

            Destroy(this.gameObject);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Player"))
    //    {
    //        // Call FreezePlayer on the freeze manager
    //        freezeManager.FreezePlayer(other.gameObject);
    //    }
    //}

}
