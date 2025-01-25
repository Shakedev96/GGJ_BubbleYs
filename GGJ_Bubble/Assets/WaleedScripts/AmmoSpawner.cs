using System.Collections.Generic;
using UnityEngine;

public class AmmoSpawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public BoxCollider spawnArea;
    public float spawnInterval = 2f; 
    public int maxSpawnCount = 10; 

    private float timer; // Timer to track spawn intervals
    private List<GameObject> activePrefabs; // List to track spawned prefabs
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

        activePrefabs = new List<GameObject>(); // Initialize the list
    }

    private void Update()
    {
        if (!readyManager.gameStarted) return;

        timer += Time.deltaTime;

        // Check if it's time to spawn and if we haven't reached the max count
        if (timer >= spawnInterval && activePrefabs.Count < maxSpawnCount)
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
            GameObject spawnedPrefab = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

            // Add the spawned prefab to the list
            activePrefabs.Add(spawnedPrefab);

            // Assign a callback to remove it when picked up
            AmmoCollectible ammoPickup = spawnedPrefab.GetComponent<AmmoCollectible>();
            if (ammoPickup != null)
            {
                ammoPickup.OnPickedUp += HandleAmmoPickedUp;
            }
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

    private void HandleAmmoPickedUp(GameObject pickedUpPrefab)
    {
        // Remove the prefab from the active list when picked up
        if (activePrefabs.Contains(pickedUpPrefab))
        {
            activePrefabs.Remove(pickedUpPrefab);
        }

        // Destroy the prefab
        Destroy(pickedUpPrefab);
    }
}
