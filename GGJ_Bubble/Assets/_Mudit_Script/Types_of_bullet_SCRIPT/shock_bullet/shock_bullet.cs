using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shock_bullet : MonoBehaviour
{
    public Bubble_shotter_bar bubble_shotter_Bar;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            bubble_shotter_Bar = collision.gameObject.GetComponent<Bubble_shotter_bar>();
            bubble_shotter_Bar.height = bubble_shotter_Bar.minHeight;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bubble_shotter_Bar = other.gameObject.GetComponent<Bubble_shotter_bar>();
            bubble_shotter_Bar.height = bubble_shotter_Bar.minHeight;
        }
    }
}
