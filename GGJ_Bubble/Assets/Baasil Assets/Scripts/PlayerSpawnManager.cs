using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawnManager : MonoBehaviour
{
    [Header("Player Prefabs")]
    [SerializeField] private GameObject[] playerPrefabs; // Array of player prefabs (assign in inspector)

    [Header("Spawn Points")]
    [SerializeField] private Transform[] spawnPoints; // Preselected spawn points (assign in inspector)

    private List<PlayerInput> players = new List<PlayerInput>(); // Track spawned players

    private void Start()
    {
        if (spawnPoints.Length < 4 || playerPrefabs.Length < 3) // We have 3 player prefabs in the array
        {
            Debug.LogError("Please assign at least 4 spawn points and 3 player prefabs in the inspector!");
        }
    }
    
    public void OnPlayerJoined(PlayerInput playerInput)
    {
        // Determine the spawn point index based on the number of players
        int playerIndex = players.Count;

        if (playerIndex < playerPrefabs.Length)
        {
            // Set player position to the corresponding spawn point
            playerInput.transform.position = spawnPoints[playerIndex].position;

            // Optionally, adjust player rotation
            playerInput.transform.rotation = spawnPoints[playerIndex].rotation;

            // Change player prefab for customization
            PlayerInputManager.instance.playerPrefab = playerPrefabs[playerIndex];

            // Add player to the list for tracking
            players.Add(playerInput);

            Debug.Log($"Player {playerIndex + 1} spawned at {spawnPoints[playerIndex].position}");
        }
        else
        {
            Debug.LogWarning("More players than spawn points! Consider increasing spawn points.");
        }
    }

}
