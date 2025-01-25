using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BubbleGums[] weapons; // Assign 4 bubblegums here
    public int currentWeaponIndex = 0;

    public Transform shootPoint; // The point from which the bullet is shot
    private float nextFireTime = 0f;
    private int maxAmmo = 5;
    private int currentAmmo;

    public BubbleGums CurrentWeapon => weapons[currentWeaponIndex]; // Get current weapon

    public void onFire(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            Shoot();
        }
    }
    public void onWeaponSwitch(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            SwitchWeapon();
        }
    }



    //Weapon Switch
    void SwitchWeapon()
    {
        // Store the initial index to avoid infinite loops
        int initialIndex = currentWeaponIndex;

        do
        {
            // Increment the weapon index
            currentWeaponIndex = (currentWeaponIndex + 1) % 4; // Wraps around since there are 4 weapons

            // Check if the current weapon has ammo
            //if (CurrentWeapon.currentAmmo > 0)
            //{
            //     // Exit the loop if the weapon has ammo
            //}
            break;

            // If we've looped through all weapons, stay on the initial weapon
            if (currentWeaponIndex == initialIndex)
            {
                Debug.LogWarning("No weapons with ammo available!");
                return;
            }

        } while (true);

        Debug.Log("Current Weapon: " + CurrentWeapon.weaponName);
    }

    //Shoot
    private void Shoot()
    {
        if (CurrentWeapon != null && CurrentWeapon.projectilePrefab != null )  //&& CurrentWeapon.currentAmmo <= maxAmmo && CurrentWeapon.currentAmmo > 0
        {
            // Instantiate the projectile at the shoot point and apply its rotation
            GameObject projectile = Instantiate(CurrentWeapon.projectilePrefab, shootPoint.position, shootPoint.rotation);

            // Initialize the projectile with the current weapon's data
            var bubblegumProjectile = projectile.GetComponent<BubbleGumsProjectile>();
            if (bubblegumProjectile != null)
            {
                bubblegumProjectile.Initialize(CurrentWeapon); // Pass the data from the selected weapon
            }

            // Set next fire time based on fire rate
            nextFireTime = Time.time + 1f / CurrentWeapon.fireRate;
        }
        else
        {
            Debug.LogWarning("No weapon or projectile prefab assigned or No Ammo!");
        }
    }
}
