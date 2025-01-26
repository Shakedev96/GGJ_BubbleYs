using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReadyManager : MonoBehaviour
{

    [SerializeField] private ready[] readyScripts;
    [SerializeField] private PlayerMovement[] playerMovement;
    [SerializeField] private PlayerAttack[] playerAttack;
    [SerializeField] private GameObject[] playerObjects;

    [SerializeField] private int playersReady = 0;
    [SerializeField] private TextMeshProUGUI countdownTimer;
    [SerializeField] private GameObject playerSpawner;

    private bool isCountdownActive = false;
    private bool[] hasBeenCounted; // Tracks whether each player has been counted
    public bool gameStarted = false; // Prevent multiple game starts



    private void Update()
    {
        ReadyPlayer();

    }
    private void ReadyPlayer()
    {
        // Get the current player objects and their ready scripts
        playerObjects = GameObject.FindGameObjectsWithTag("Player");
        readyScripts = new ready[playerObjects.Length];

        if (hasBeenCounted == null || hasBeenCounted.Length != playerObjects.Length)
        {
            // Initialize the tracking array if needed
            hasBeenCounted = new bool[playerObjects.Length];
        }

        for (int i = 0; i < playerObjects.Length; i++)
        {
            readyScripts[i] = playerObjects[i].GetComponent<ready>();

            // Check if the player is ready and hasn't been counted yet
            if (readyScripts[i].isReady && !hasBeenCounted[i])
            {
                playersReady++; // Increment the ready count
                hasBeenCounted[i] = true; // Mark the player as counted
            }
        }

        // Start the countdown if all players are ready
        if (playersReady == playerObjects.Length && !isCountdownActive && playersReady > 1)
        {
            StartCoroutine(StartCountdown());

        }
    }

    // Countdown coroutine
    private IEnumerator StartCountdown()
    {
        isCountdownActive = true;

        int countdownValue = 3;

        while (countdownValue > 0)
        {
            countdownTimer.text = countdownValue.ToString(); // Update countdown text
            yield return new WaitForSeconds(1f); // Wait for 1 second
            countdownValue--; // Decrease the countdown
        }

        countdownTimer.text = "GO!"; // Display "GO!" after countdown finishes
        yield return new WaitForSeconds(1f); // Display "GO!" for a short time

        GameStart(); // Trigger the game start

    }

    // This function gets called when the countdown finishes
    private void GameStart()
    {
        if (gameStarted) return; // Prevent multiple game starts
        gameStarted = true;

        Debug.Log("Game Started!");
        playersReady = 0; // Reset player-ready counter
        countdownTimer.text = ""; // Clear countdown text

        playerSpawner.SetActive(false); // STOP PLAYER SPAWNING AFTER GAME STARTS

        playerMovement = new PlayerMovement[playerObjects.Length];
        playerAttack = new PlayerAttack[playerObjects.Length];

        for (int i = 0; i < playerObjects.Length; i++)
        {
            playerMovement[i] = playerObjects[i].GetComponent<PlayerMovement>();
            playerAttack[i] = playerObjects[i].GetComponent<PlayerAttack>();

            playerMovement[i].canMove = true; // Enable player movement
            playerAttack[i].can_shoot = true; // Enable player shotting

            readyScripts[i].enabled = false; // Disable ready script
        }
    }

}
