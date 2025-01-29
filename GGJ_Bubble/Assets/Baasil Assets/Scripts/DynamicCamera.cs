using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCamera : MonoBehaviour
{
    //[Header("Follow Settings")]
    //[SerializeField] private float followSpeed = 5f; // Speed at which the camera moves while maintaining offset

    //[Header("Player Settings")]
    //private List<GameObject> playerObjects = new List<GameObject>(); // List of player GameObjects

    //private void LateUpdate()
    //{
    //    if (playerObjects.Count == 0) return; // Do nothing if no players

    //    MoveCamera();
    //}

    //private void MoveCamera()
    //{
    //    if (playerObjects.Count > 1)
    //    {
    //        // If 2 or more players, move the camera to look at their midpoint
    //        Vector3 targetPosition = GetAveragePlayerPosition();
    //        targetPosition.y = transform.position.y; // Maintain original Y position
    //        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * followSpeed);

    //    }

    //}

    //private Vector3 GetAveragePlayerPosition()
    //{
    //    Vector3 sumPosition = Vector3.zero;
    //    int validPlayers = 0;

    //    foreach (GameObject player in playerObjects)
    //    {
    //        if (player != null)
    //        {
    //            sumPosition += new Vector3(player.transform.position.x, 0, player.transform.position.z); // Ignore Y
    //            validPlayers++;
    //        }
    //    }

    //    Vector3 averagePosition = sumPosition / validPlayers;
    //    averagePosition.y = transform.position.y; // Keep the camera's current Y

    //    return averagePosition;
    //}

    //// Called when a new player joins
   


    //--------------------------------------------------------------------Mudit---script----starts---here----------------------------------------------

    public GameObject[] ALL_The_Player;
    public Camera Main_camera;
    Vector3 centerPoint;

    public float minFOV; // Minimum field of view (zoomed in)
    public float maxFOV; // Maximum field of view (zoomed out)
    public float zoomSpeed ; // Speed of zoom

    public float zoomDistanceThreshold ; // Distance threshold for zooming


    private void LateUpdate()
    {
        centerPoint = FindCenterPoint(ALL_The_Player);

        transform.LookAt(centerPoint);

        // Calculate the distance between the players
        float playerDistance = CalculatePlayerDistance();

        // Adjust the camera's Field of View (FOV) based on distance
        AdjustCameraZoom(playerDistance);

        //Debug.LogAssertion(centerPoint);
    }

    Vector3 FindCenterPoint(GameObject[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            Debug.LogWarning("No objects provided.");
            return Vector3.zero;
        }

        Vector3 sum = Vector3.zero;

        // Sum the positions of all GameObjects
        foreach (var obj in objects)
        {
            sum += obj.transform.position;
        }

        // Return the average position (center point)
        return sum / objects.Length;
    }

    public void UpdatePlayerGameObjects()
    {
        // Find all players with the "Player" tag
        GameObject[] foundPlayers = GameObject.FindGameObjectsWithTag("Player");

        // Update the array with the new players
        ALL_The_Player = new GameObject[foundPlayers.Length];
        for (int i = 0; i < foundPlayers.Length; i++)
        {
            ALL_The_Player[i] = foundPlayers[i];
        }
    }
    float CalculatePlayerDistance()
    {
        if (ALL_The_Player.Length < 2)
        {
            return 0f; // No valid distance if there are fewer than 2 players
        }

        Vector3 firstPlayerPosition = ALL_The_Player[0].transform.position;
        Vector3 lastPlayerPosition = ALL_The_Player[ALL_The_Player.Length - 1].transform.position;

        // Calculate the distance between the first and last player
        return Vector3.Distance(firstPlayerPosition, lastPlayerPosition);
    }

    void AdjustCameraZoom(float playerDistance)
    {
        // If the players are closer than the threshold distance, zoom in
        if (playerDistance < zoomDistanceThreshold)
        {
            Main_camera.fieldOfView = Mathf.Lerp(Main_camera.fieldOfView, minFOV, Time.deltaTime * zoomSpeed);
        }
        else // Otherwise, zoom out
        {
            Main_camera.fieldOfView = Mathf.Lerp(Main_camera.fieldOfView, maxFOV, Time.deltaTime * zoomSpeed);
        }
    }
}




