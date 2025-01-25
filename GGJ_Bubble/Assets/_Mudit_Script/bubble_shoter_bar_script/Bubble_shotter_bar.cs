using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bubble_shotter_bar : MonoBehaviour
{
    [SerializeField] private Image bar; // The Image for the bar
    [SerializeField] private float growSpeed = 2f; // Speed of bar growth
    [SerializeField] private float maxHeight = 300f; // Maximum height of the bar
    [SerializeField] private float minHeight = 50f; // Minimum height of the bar
    [SerializeField] private InputActionReference controlActionShoot; // Input for shooting
    [SerializeField] private InputActionReference controlActionAim; // Input for aiming

    private bool isGrowing = true; // Determines if the bar is growing
    private bool isStopped = false; // Tracks if the bar has been stopped
    private float powerLevel = 0f; // Tracks the power level

    public bool shooting;

    private void OnEnable()
    {
        // Enable input actions
        controlActionShoot.action.Enable();
        controlActionAim.action.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        controlActionShoot.action.Disable();
        controlActionAim.action.Disable();
    }

    private void Update()
    {
        // Check if the player is holding the aim input
        if (controlActionAim.action.IsPressed())
        {
            // Call the bar growth-shrink logic
            bar_up_and_down();
        }
        else
        {
            // Reset if the aim action is released
            isStopped = false;
            ResetBarHeight();
        }
    }

    private void bar_up_and_down()
    {
        if (!isStopped)
        {
            // Grow and shrink the bar
            float height = bar.rectTransform.sizeDelta.y;
            if (isGrowing)
            {
                height += growSpeed * Time.deltaTime;
                if (height >= maxHeight)
                {
                    height = maxHeight;
                    isGrowing = false; // Switch to shrinking
                }
            }
            else
            {
                height -= growSpeed * Time.deltaTime;
                if (height <= minHeight)
                {
                    height = minHeight;
                    isGrowing = true; // Switch to growing
                }
            }

            // Apply height changes to the bar's RectTransform
            bar.rectTransform.sizeDelta = new Vector2(bar.rectTransform.sizeDelta.x, height);
        }

        // Stop the bar and calculate the power level when shoot input is pressed
        if (controlActionShoot.action.WasPressedThisFrame())
        {
            isStopped = true; // Stop the bar
            DeterminePowerLevel(); // Calculate the power level
            ResetBarHeight();
        }
    }

    private void DeterminePowerLevel()
    {
        float height = bar.rectTransform.sizeDelta.y;

        if (height <= minHeight + (maxHeight - minHeight) / 3)
        {
            powerLevel = 10; // Level 1
        }
        else if (height <= minHeight + 2 * (maxHeight - minHeight) / 3)
        {
            powerLevel = 20; // Level 2
        }
        else
        {
            powerLevel = 30; // Level 3
        }

        // Output the power level
        Debug.Log($"Power Level: {powerLevel}");
    }
    private void ResetBarHeight()
    {
        // Reset the bar's height to minimum
        bar.rectTransform.sizeDelta = new Vector2(bar.rectTransform.sizeDelta.x, minHeight);
    }
}
