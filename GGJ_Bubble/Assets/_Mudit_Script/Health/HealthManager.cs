using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    [SerializeField] private Image healthBar;
    [SerializeField] private float maxHealth;

    public float currentHealth;

    [SerializeField] private DynamicCamera dynamicCamera;

    void Start()
    {
        currentHealth = maxHealth;
        updateHealth();

    }

    public void damageHealth(float damage)
    {
        if (currentHealth > 0)
        {
            // Deduct health
            currentHealth -= damage;

            // Display health
            updateHealth();

            // Check Death
            checkDeath();

        }



    }

    public void healHealth(float heal)
    {
        if (currentHealth < maxHealth)
        {
            // Add health
            currentHealth += heal;

            // Display health
            updateHealth();

        }

    }

    private void updateHealth()
    {
        // Clamp health 0-max
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Set Bar
        healthBar.fillAmount = currentHealth / maxHealth;

    }


    private void checkDeath()
    {
        // Check death
        if (currentHealth <= 0)
        {
            // Hide the player 
            gameObject.SetActive(false);

            // Remove player from camera
            Invoke("destroyPlayerCamera", 0.1f);

            // Handle death
            Invoke("destroyPlayer", 0.2f);

        }


    }

    private void destroyPlayerCamera()
    {
        dynamicCamera = GameObject.FindWithTag("MainCamera").GetComponent<DynamicCamera>();
        dynamicCamera.UpdatePlayerGameObjects(); // Call the function to update the player list

    }

    private void destroyPlayer()
    {
        Destroy(gameObject);
    }

}


