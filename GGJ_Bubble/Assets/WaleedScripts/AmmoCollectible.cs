using System;
using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public string weaponName;
    public event Action<GameObject> OnPickedUp;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerWeaponManager weaponManager = collision.GetComponent<PlayerWeaponManager>();

            if (weaponManager != null)
            {
                // Check if ammo can be added before picking up
                if (weaponManager.CanAddAmmo(weaponName))
                {
                    // Add ammo to the corresponding weapon
                    weaponManager.AddAmmo(weaponName);

                    // Trigger any OnPickedUp event and destroy the collectible
                    OnPickedUp?.Invoke(gameObject);
                    Debug.Log("Ammo +1 for " + weaponName);
                    Destroy(gameObject);
                }
                else
                {
                    Debug.Log("Cannot pick up ammo, max ammo reached for " + weaponName);
                }
            }
        }
    }
}
