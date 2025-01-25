using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSpeed = 5f; // Movement speed
    public bool canMove = true;
    private Rigidbody rb;
    private Vector3 moveDirection;
    public Vector2 moveInput; // Stores the movement input

    [Header("Rotate")]
    private Vector2 rotateInput; // Stores input for rotation
    public float rotationSmoothTime = 0.1f; // Smoothing for rotation
    private float rotationVelocity; // Helper for smoothing
    private float targetAngle; // Target angle for rotation

    [Header("Jump")]
    public float jumpForce = 5f; // Force applied when jumping
    public bool isGrounded; // Check if the player is on the ground
    [SerializeField] private float groundCheckRadius = 0.3f; // Radius for sphere cast
    [SerializeField] private float groundCheckDistance = 0.3f; // Distance for sphere offset
    [SerializeField] private LayerMask groundLayer; // Layer to detect ground

    [Header("Dash")]
    [SerializeField] private float dashForce = 10f; // Total force applied during dash
    [SerializeField] private float dashDuration = 0.2f; // Time the dash lasts
    [SerializeField] private float dashCooldown = 1f; // Time before the player can dash again
    public bool isDashing = false;
    private bool canDash = true;
    private float dashTimeElapsed = 0f; // Tracks time passed during the dash
    private Vector3 dashDirection; // Direction of the dash

    private void Start()
    {
        rb = GetComponent<Rigidbody>();


    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    public void onRotate(InputAction.CallbackContext context)
    {
        rotateInput = context.ReadValue<Vector2>(); // Map the rotation input (Right Stick)

    }

    public void onJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            handleJump();
        }


    }

    public void onDash(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            HandleDash();
        }

    }

    private void FixedUpdate()
    {
        // Handle movement and rotation
        handleMovement();
        handleRotation();


        if (isDashing)
        {
            PerformDash();
        }
    }

    private void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics.CheckSphere(transform.position - new Vector3(0, groundCheckDistance, 0), groundCheckRadius, groundLayer);

    }

    private void handleMovement()
    {
        if (canMove)
        {
            // Calculate the movement direction
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y);

            // Apply movement using Rigidbody's velocity
            rb.velocity = new Vector3(moveDirection.x * moveSpeed, rb.velocity.y, moveDirection.z * moveSpeed);
        }
        else
        {
            // Stop the Rigidbody when movement is not allowed
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }

    }
    private void PerformDash()
    {
        if (dashTimeElapsed < dashDuration)
        {
            // Smoothly apply dash force over time
            float dashStep = (dashForce / dashDuration) * Time.fixedDeltaTime;
            rb.velocity = dashDirection * dashStep;

            // Increment the dash timer
            dashTimeElapsed += Time.fixedDeltaTime;
        }
        else
        {
            // End dash
            isDashing = false;
            canMove = true;

            // Reset velocity to ensure smooth transition
            rb.velocity = Vector3.zero;

            // Start dash cooldown
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }


    private void handleRotation()
    {
        if (rotateInput.magnitude >= 0.5f) // Check if input magnitude is above threshold
        {
            // Calculate target rotation angle (relative to world space)
            targetAngle = Mathf.Atan2(rotateInput.x, rotateInput.y) * Mathf.Rad2Deg;

            // Smoothly rotate towards the target angle
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationVelocity, rotationSmoothTime);

            // Apply rotation to the player's transform
            transform.rotation = Quaternion.Euler(0, smoothAngle, 0);

        }

    }

    private void handleJump()
    {
        if (isGrounded)
        {
            // Apply jump force
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

            Debug.Log("Jumped");

        }
    }

    private void HandleDash()
    {
        if (canDash && !isDashing)
        {
            // Set dash state
            isDashing = true;
            canDash = false;

            // Disable movement during dash
            canMove = false;

            // Set the dash direction to the player's forward direction
            dashDirection = transform.forward;

            // Reset dash timer
            dashTimeElapsed = 0f;
        }
    }

    // Reset dash availability after cooldown
    private void ResetDash()
    {
        canDash = true;
    }

    private void OnDrawGizmosSelected()
    {
        // Draw the sphere for visualization in the editor
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position - new Vector3(0, groundCheckDistance, 0), groundCheckRadius);

    }

}