using UnityEngine;

[CreateAssetMenu(fileName = "NewBubblegum", menuName = "Weapons/Bubblegum")]
public class BubbleGums : ScriptableObject
{
    public string weaponName;
    public GameObject projectilePrefab;
    public float fireRate; // Bullets per second
    public float speed;
    public int baseDamage;
    public float destroyTime;
    public int currentAmmo;

    // Special Effects
    public bool slowsPlayer; // If true, slows down the player
    public float slowAmount; // Percentage (e.g., 0.5 for 50% slow)

    public bool areaDamage; // If true, deals area damage
    public float areaRadius; // Radius of the area effect

    public bool freezesPlayer; // If true, freezes the player for a duration
    public float freezeDuration; // Duration of the freeze
}
