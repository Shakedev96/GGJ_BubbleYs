using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ready : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public bool isReady = false;

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    public void onReady(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isReady = !isReady;  // Toggle the ready state
        }
       

    }

}
