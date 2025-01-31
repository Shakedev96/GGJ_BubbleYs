using UnityEngine;

public class BubbleGumsProjectile : MonoBehaviour
{
    public int damage;
    public float speed;
    private float areaRadius;
    private bool causesSlow;
    private bool causesFreeze;
    private float slowAmount;
    private float freezeDuration;
    private float destroyTime;

    public float powerSpeed;


    private BubbleGums weaponData; // Reference to the weapon's data from the ScriptableObject



    public void Initialize(BubbleGums bubblegumData, float powerlevel)
    {
        // Initialize the projectile with data from the selected Bubblegum weapon
        weaponData = bubblegumData;

        // Set properties based on the selected weapon's data
        damage = weaponData.baseDamage;
        speed = weaponData.speed;
        areaRadius = weaponData.areaRadius;
        causesSlow = weaponData.slowsPlayer;
        causesFreeze = weaponData.freezesPlayer;
        slowAmount = weaponData.slowAmount;
        freezeDuration = weaponData.freezeDuration;
        destroyTime = weaponData.destroyTime;

        // Apply power multiplier when initializing
        powerSpeed = speed * powerlevel;

        Debug.Log("Projectile Initialized - Power Speed: " + powerSpeed);
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector3.forward * powerSpeed * Time.deltaTime);
        Destroy(gameObject,destroyTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Handle collision with enemies
        //if (collision.collider.CompareTag("Enemy"))
        //{
        //    ApplyDamage(collision.collider.gameObject);

        //    if (causesSlow)
        //    {
        //        ApplySlow(collision.collider.gameObject);
        //    }

        //    if (causesFreeze)
        //    {
        //        ApplyFreeze(collision.collider.gameObject);
        //    }

        //    if (areaRadius > 0)
        //    {
        //        ApplyAreaDamage(collision.transform.position);
        //    }

            
        //}
    }

    //private void ApplyDamage(GameObject target)
    //{
    //    // Apply damage to the target
    //    // You can access an enemy health script and subtract health here
    //    Debug.Log($"Dealing {damage} damage to {target.name}");
    //}

    //private void ApplySlow(GameObject target)
    //{
    //    // Apply slow effect to the target
    //    Debug.Log($"Slowing {target.name} by {slowAmount * 100}%");
    //    // Slow logic can be implemented on the target’s movement script
    //}

    //private void ApplyFreeze(GameObject target)
    //{
    //    // Apply freeze effect to the target
    //    Debug.Log($"Freezing {target.name} for {freezeDuration} seconds");
    //    // Freeze logic can be implemented on the target’s movement script (stop movement for a set time)
    //}

    //private void ApplyAreaDamage(Vector3 position)
    //{
    //    // Area damage logic (for enemies within area radius)
    //    Collider[] hitColliders = Physics.OverlapSphere(position, areaRadius);
    //    foreach (var hit in hitColliders)
    //    {
    //        if (hit.CompareTag("Enemy"))
    //        {
    //            // Apply damage to the enemy
    //            Debug.Log($"Dealing area damage to {hit.name}");
    //            // Apply area damage (same as ApplyDamage)
    //        }
    //    }
    //}
}
