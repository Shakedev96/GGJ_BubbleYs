using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject[] prefabs; 
    public BoxCollider spawnArea; 
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 5f;

    private float spawnTimer; // Timer to track time until next spawn
    private float nextSpawnTime; // Randomized time for the next spawn
    [SerializeField] private ReadyManager readyManager;

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

        SetRandomSpawnTime(); // Set the initial spawn time
    }

    private void Update()
    {
        if (!readyManager.gameStarted) return;

        spawnTimer += Time.deltaTime;

        // Check if it's time to spawn a new item
        if (spawnTimer >= nextSpawnTime)
        {
            SpawnRandomPrefab();
            spawnTimer = 0f; // Reset the timer
            SetRandomSpawnTime(); // Set a new random spawn interval
        }
    }

    private void SpawnRandomPrefab()
    {
        if (prefabs.Length > 0 && spawnArea != null)
        {
            Vector3 spawnPosition = GetRandomPositionInCollider();

            // Select a random prefab to spawn
            int randomIndex = Random.Range(0, prefabs.Length);
            GameObject prefabToSpawn = prefabs[randomIndex];

            // Instantiate the selected prefab
            Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomPositionInCollider()
    {
        Bounds bounds = spawnArea.bounds;

        // Generate a random position within the bounds of the collider
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, y, z);
    }

    private void SetRandomSpawnTime()
    {
        // Randomize the interval for the next spawn
        nextSpawnTime = Random.Range(minSpawnInterval, maxSpawnInterval);
    }
}
