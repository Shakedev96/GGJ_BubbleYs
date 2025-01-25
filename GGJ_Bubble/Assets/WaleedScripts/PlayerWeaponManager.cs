using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponManager : MonoBehaviour
{
    public List<BubbleGums> weapons;

    private void Start()
    {
        foreach (BubbleGums weapon in weapons)
        {
            weapon.currentAmmo = 2; 
        }
    }

    public void AddAmmo(string weaponName)
    {
        // Find the weapon by name and increase its ammo
        foreach (BubbleGums weapon in weapons)
        {
            if (weapon.weaponName == weaponName)
            {
                weapon.currentAmmo += 1;
                return;
            }
        }
    }
}
