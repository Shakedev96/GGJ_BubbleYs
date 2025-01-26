using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionArrow : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float heightOffset = 0.1f; // Adjust to keep the arrow slightly above the ground
    public float widthOffset = 0f; // Adjust to move the arrow left or right of the player

    [SerializeField] private Transform selectionRing; // The selection ring GameObject under the player

    void Start()
    {
        if (player == null)
        {
            Debug.LogError("Player transform is not assigned!");
            return;
        }

        // Create and position the selection ring as a child of the player
        selectionRing = new GameObject("SelectionRing").transform;
        selectionRing.SetParent(player);
        selectionRing.localPosition = Vector3.zero; // Position it at the player's feet
        selectionRing.localScale = new Vector3(1, 1, 1); // Adjust scale if needed
    }

    void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player transform not assigned!");
            return;
        }

        // Calculate the width offset based on the player's local forward direction
        Vector3 offset = player.forward * widthOffset;

        // Set the arrow's position relative to the player's position
        transform.position = player.position + offset + new Vector3(0, heightOffset, 0);

        // Rotate the arrow to point in the player's local forward direction
        transform.rotation = Quaternion.LookRotation(player.forward);
    }
}
