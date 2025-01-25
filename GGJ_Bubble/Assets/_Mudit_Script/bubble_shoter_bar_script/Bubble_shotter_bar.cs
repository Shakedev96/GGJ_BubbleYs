using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Bubble_shotter_bar : MonoBehaviour
{
    [SerializeField] private Image bar; // The Image for the bar
    [SerializeField] private float growSpeed = 2f; // Speed of bar growth
    [SerializeField] private float maxHeight = 300f; // Maximum height of the bar
    [SerializeField] private float minHeight = 50f; // Minimum height of the bar

    private bool isGrowing = true; // Determines if the bar is growing or shrinking
    private float powerLevel = 0f;

    public bool shooting; // Whether the player is shooting
    public bool aiming;   // Whether the player is aiming

    // Called when the aim button is pressed or released
    public void onAim(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            aiming = true; // Start aiming
        }
        else if (context.canceled)
        {
            aiming = false; // Stop aiming
            ResetBarHeight(); // Reset bar height when aim is released
        }
    }

    // Called when the shoot button is pressed
    public void onShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            shooting = true; // Shooting has occurred
            DeterminePowerLevel(); // Determine power level based on current bar height
            ResetBarHeight(); // Reset the bar height after shooting
            shooting = false; // Reset shooting flag
        }
    }

    // Called every frame to update the bar
    private void Update()
    {
        if (aiming)
        {
            // Oscillate the bar up and down when aiming
            OscillateBar();
        }
    }

    private void OscillateBar()
    {
        if (aiming)
        {
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
    }

    public void DeterminePowerLevel()
    {
        float height = bar.rectTransform.sizeDelta.y;

        // Determine power level based on the bar's height
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

    // Reset the bar's height to minimum after shooting or when aim is released
    public void ResetBarHeight()
    {
        bar.rectTransform.sizeDelta = new Vector2(bar.rectTransform.sizeDelta.x, minHeight);
    }

}
