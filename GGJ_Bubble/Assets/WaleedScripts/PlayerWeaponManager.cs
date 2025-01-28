using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<BubbleGums> weapons;
    private const int MaxAmmo = 5; // Define the maximum ammo value

    private void Start()
    {
        foreach (BubbleGums weapon in weapons)
        {
            //weapon.currentAmmo = 2; // Initialize ammo count
        }
    }

    public void AddAmmo(string weaponName)
    {
        // Find the weapon by name and increase its ammo
        foreach (BubbleGums weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                //if () //current ammo from player attack)
                //{
                //    //current ammo from player attack += 1;
                //    Debug.Log(weapon.weaponName + " ammo increased to " + );//currentammo from player attack);
                //}
                //else
                //{
                //    Debug.Log(weapon.weaponName + " already has max ammo.");
                //}
                return;
            }
        }
    }

    public bool CanAddAmmo(string weaponName)
    {
        // Check if ammo can be added for the specified weapon
        foreach (BubbleGums weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                //return weapon.currentAmmo < MaxAmmo;
            }
        }
        return false; // If weapon is not found, return false
    }
}
