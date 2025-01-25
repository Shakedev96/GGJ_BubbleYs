using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float speed = 5f;
    private Rigidbody rb;
    public Camera mainCamera;

    public BubbleGums[] weapons; // Assign 4 bubblegums here
    public int currentWeaponIndex = 0;

    public Transform shootPoint; // The point from which the bullet is shot
    private float nextFireTime = 0f;
    private int maxAmmo = 5;
    private int currentAmmo;

    public BubbleGums CurrentWeapon => weapons[currentWeaponIndex]; // Get current weapon
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //Move
        TempMove();

        //Aim
        Aim();

        // Switch Weapon
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }

        //Shoot
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime) // Left-click
        {
            Shoot();
        }

        if (Input.GetAxis("Fire1") > 0.1f && Time.time >= nextFireTime) // Gamepad button (Left Trigger)
        {
            Shoot();
        }
        Debug.Log("Current Weapon : " + CurrentWeapon.name);
        Debug.Log("Ammo : " + CurrentWeapon.currentAmmo);
    }


    //Temporary Movement
    void TempMove()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");     

        Vector3 forward = mainCamera.transform.forward;
        Vector3 right = mainCamera.transform.right;

        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;

        transform.position += moveDirection * speed * Time.deltaTime;
    }


    //Aim
    void Aim()
    {
        var (success,position) = GetMousePosition();
        if(success)
        {
            var direction = position - transform.position;

            direction.y = 0;

            transform.forward = direction;
        }
    }
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(ray, out var hitInfo, Mathf.Infinity))
        {
            return (success: true, position: hitInfo.point);
        }
        else
        {
            return (success: false, position: Vector3.zero);
        }
    }

    //Weapon Switch
    void SwitchWeapon()
    {
        currentWeaponIndex = (currentWeaponIndex + 1);

        if (currentWeaponIndex >= 4)
        {
            currentWeaponIndex = 0;
        }

        Debug.Log("Current Weapon : " + CurrentWeapon.weaponName);
    }

    //Shoot
    private void Shoot()
    {
        if (CurrentWeapon != null && CurrentWeapon.projectilePrefab != null && CurrentWeapon.currentAmmo <= maxAmmo && CurrentWeapon.currentAmmo > 0)
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
            CurrentWeapon.currentAmmo--;
        }
        else
        {
            Debug.LogWarning("No weapon or projectile prefab assigned or No Ammo!");
        }
    }
}
