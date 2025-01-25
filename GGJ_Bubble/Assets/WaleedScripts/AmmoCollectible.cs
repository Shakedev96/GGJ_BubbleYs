using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public string weaponName;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWeaponManager weaponManager = collision.GetComponent<PlayerWeaponManager>();

            if (weaponManager != null)
            {
                // Add ammo to the corresponding weapon
                weaponManager.AddAmmo(weaponName);
            }

            // Destroy the collectible after it's collected
            Debug.Log("Ammo +1 for " + weaponName);
            Destroy(gameObject);
        }
    }
}
