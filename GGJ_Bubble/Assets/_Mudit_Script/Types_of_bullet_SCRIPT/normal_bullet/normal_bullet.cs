using UnityEngine;

public class normal_bullet : MonoBehaviour
{
    private HealthManager healthmanager;
    public BubbleGums bubblegums;

    private Bubble_shotter_bar powerBarlevel;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            healthmanager = collision.gameObject.GetComponent<HealthManager>();
            powerBarlevel = collision.gameObject.GetComponent<Bubble_shotter_bar>();

            healthmanager.damageHealth(bubblegums.baseDamage * powerBarlevel.powerLevel);
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
