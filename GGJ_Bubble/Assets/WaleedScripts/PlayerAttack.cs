using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public BubbleGums[] weapons; // Assign 4 bubblegums here
    public int currentWeaponIndex = 0;

    public Transform shootPoint; // The point from which the bullet is shot
    private float nextFireTime = 0f;

    private int[] currentAmmo; // Ammo count for each weapon (tracked locally)
    private const int MaxAmmo = 5; // Maximum ammo for each weapon

    private Animator anim;
    private Bubble_shotter_bar bubble_Shotter_Bar;

    public bool can_shoot = true;
    public bool isShocked = false;

    private HealthManager healthManager;


    public BubbleGums CurrentWeapon => weapons[currentWeaponIndex]; // Get the current weapon

    private void Start()
    {
        anim = GetComponent<Animator>();
        bubble_Shotter_Bar = GetComponent<Bubble_shotter_bar>();

        healthManager = GetComponent<HealthManager>();

        // Initialize ammo array with starting ammo
        currentAmmo = new int[weapons.Length];
        for (int i = 0; i < weapons.Length; i++)
        {
            currentAmmo[i] = 2; // Set initial ammo to max (5) for all weapons
        }


    }
    private void Update()
    {
        // Automatically switch weapon if the current one is out of ammo
        if (currentAmmo[currentWeaponIndex] <= 0)
        {
            SwitchWeapon();
        }
    }

    public void onFire(InputAction.CallbackContext context)
    {


        if (context.performed)
        {
            if (bubble_Shotter_Bar.aiming)
            {
                if (can_shoot)
                {
                    Shoot();

                    /*bubble_Shotter_Bar.isStopped = true;*/ // Stop the bar
                    bubble_Shotter_Bar.DeterminePowerLevel(); // Calculate the power level
                    bubble_Shotter_Bar.ResetBarHeight();
                }
                else if (isShocked)
                {
                    healthManager.damageHealth(10);

                }


            }

        }



    }


    public void onWeaponSwitch(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            SwitchWeapon();
        }
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AmmoCollectible"))
        {
            AmmoCollectible collectible = collision.gameObject.GetComponent<AmmoCollectible>();
            if (collectible != null)
            {
                CollectAmmo(collectible);
            }
        }
    }

    // Weapon Switch Logic
    private void SwitchWeapon()
    {
        int initialIndex = currentWeaponIndex;

        do
        {
            // Increment the weapon index and wrap around if necessary
            currentWeaponIndex = (currentWeaponIndex + 1) % weapons.Length;

            // Exit the loop if the selected weapon has ammo
            if (currentAmmo[currentWeaponIndex] > 0)
            {
                Debug.Log("Switched to weapon: " + CurrentWeapon.weaponName);
                return;
            }

            // If we've looped through all weapons and found no ammo, stay on the initial weapon
            if (currentWeaponIndex == initialIndex)
            {
                Debug.LogWarning("No weapons with ammo available!");
                return;
            }

        } while (true);
    }

    // Shooting Logic
    private void Shoot()
    {
        if (CurrentWeapon != null && CurrentWeapon.projectilePrefab != null)
        {
            // Check if the weapon has ammo and if enough time has passed since the last shot
            if (currentAmmo[currentWeaponIndex] > 0 && Time.time >= nextFireTime)
            {

                // Instantiate the projectile at the shoot point
                GameObject projectile = Instantiate(CurrentWeapon.projectilePrefab, shootPoint.position, shootPoint.rotation);

                // Initialize the projectile with the current weapon's data
                var bubblegumProjectile = projectile.GetComponent<BubbleGumsProjectile>();
                if (bubblegumProjectile != null)
                {
                    bubblegumProjectile.Initialize(CurrentWeapon); // Pass the data from the selected weapon
                }

                // Deduct one ammo and set the next fire time
                currentAmmo[currentWeaponIndex]--;
                nextFireTime = Time.time + 1f / CurrentWeapon.fireRate;

                // Handle animation
                anim.SetTrigger("IsShoot");


                Debug.Log($"Shot fired! Remaining ammo for {CurrentWeapon.weaponName}: {currentAmmo[currentWeaponIndex]}");
            }
            else if (currentAmmo[currentWeaponIndex] <= 0)
            {
                Debug.LogWarning($"No ammo left for {CurrentWeapon.weaponName}!");
            }
            else
            {
                Debug.LogWarning("Weapon is on cooldown!");
            }
        }
        else
        {
            Debug.LogWarning("No weapon or projectile prefab assigned!");
        }
    }

    // Ammo Collection Logic
    private void CollectAmmo(AmmoCollectible collectible)
    {
        // Find the weapon by name and add ammo if possible
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i].weaponName == collectible.weaponName)
            {
                if (currentAmmo[i] < MaxAmmo)
                {
                    currentAmmo[i]++;
                    Debug.Log($"Collected ammo for {weapons[i].weaponName}. Current ammo: {currentAmmo[i]}");
                    Destroy(collectible.gameObject); // Destroy the collectible
                }
                else
                {
                    Debug.Log($"Cannot pick up ammo for {weapons[i].weaponName}, max ammo reached!");
                }
                return;
            }
        }

        Debug.LogWarning($"No matching weapon found for collectible: {collectible.weaponName}");
    }


    public void reset_shooting_function()
    {
        can_shoot = true;
        isShocked = false;
    }

}
