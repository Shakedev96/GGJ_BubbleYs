using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject[] prefabs; // Array to hold the 4 prefabs
    public BoxCollider spawnArea; // Box collider defining the spawn area
    public float spawnInterval = 2f; // Time interval between spawns

    private float timer; // Timer to track spawn intervals

    private void Start()
    {
        if (spawnArea == null)
        {
            Debug.LogError("No spawn area assigned! Please assign a BoxCollider.");
        }
        if (prefabs.Length == 0)
        {
            Debug.LogError("No prefabs assigned! Please assign at least one prefab.");
        }
    }

    private void Update()
    {
        timer += Time.deltaTime;

        // Check if it's time to spawn
        if (timer >= spawnInterval)
        {
            SpawnRandomPrefab();
            timer = 0f; // Reset timer
        }
    }

    private void SpawnRandomPrefab()
    {
        if (prefabs.Length > 0 && spawnArea != null)
        {
            // Get a random position inside the collider bounds
            Vector3 spawnPosition = GetRandomPositionInCollider();

            // Select a random prefab
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[randomIndex];

            // Instantiate the selected prefab at the random position
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionInCollider()
    {
        Bounds bounds = spawnArea.bounds;

        // Generate random position within bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }
}
