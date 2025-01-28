using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ready : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public bool isReady = false;

    [SerializeField] private GameObject playerIndicator;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void onReady(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
                isReady = !isReady;  // Toggle the ready state

                // Enable or disable the indicator based on isReady

                playerIndicator.SetActive(isReady);


        }


    }

}
