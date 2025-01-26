using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    [Header("Zoom Settings")]
    [SerializeField] private float maxZoom = 80f; // Maximum FOV when players are far
    [SerializeField] private float minZoom = 40f; // Minimum FOV when players are close
    [SerializeField] private float defaultZoom = 60f; // Default FOV when no players
    [SerializeField] private float zoomSpeed = 5f; // Speed of zoom adjustment
    [SerializeField] private float distanceThreshold = 10f; // Distance at which the zoom adjusts

    [Header("Player Settings")]
    [SerializeField] private Transform mapCenter; // The center point of the map
    private List<GameObject> playerObjects = new List<GameObject>(); // List of player GameObjects

    private Camera cam;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        cam.fieldOfView = defaultZoom; // Set the camera to default zoom at start
    }

    private void LateUpdate()
    {
        // If no players, keep the FOV at default
        if (playerObjects.Count == 0)
        {
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, defaultZoom, Time.deltaTime * zoomSpeed);
            return;
        }

        // Calculate the maximum distance of players from the map center
        float maxDistance = GetMaxPlayerDistance();

        // Adjust FOV based on the maximum distance
        float targetFOV = Mathf.Lerp(minZoom, maxZoom, maxDistance / distanceThreshold);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, targetFOV, Time.deltaTime * zoomSpeed);
    }

    private float GetMaxPlayerDistance()
    {
        float maxDistance = 0f;

        foreach (GameObject player in playerObjects)
        {
            if (player != null) // Ensure player still exists
            {
                float distance = Vector3.Distance(player.transform.position, mapCenter.position);
                maxDistance = Mathf.Max(maxDistance, distance);
            }
        }

        return maxDistance;
    }

    public void UpdatePlayerGameObjects()
    {
        // Refresh the playerObjects list by finding all objects with the "Player" tag
        playerObjects.Clear(); // Clear the existing list
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");

        foreach (GameObject player in foundPlayers)
        {
            playerObjects.Add(player);
        }
    }

}
