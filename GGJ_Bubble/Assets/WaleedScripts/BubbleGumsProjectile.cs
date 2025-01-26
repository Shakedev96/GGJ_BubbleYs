using UnityEngine;

public class BubbleGumsProjectile : MonoBehaviour
{
    private float speed;
    private float destroyTime;


    private BubbleGums weaponData; // Reference to the weapon's data from the ScriptableObject

    public void Initialize(BubbleGums bubblegumData)
    {
        // Initialize the projectile with data from the selected Bubblegum weapon
        weaponData = bubblegumData;

        // Set properties based on the selected weapon's data
        speed = weaponData.speed;
        destroyTime = weaponData.destroyTime;
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject,destroyTime);
    }

}
