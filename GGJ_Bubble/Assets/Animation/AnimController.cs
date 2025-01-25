using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{[SerializeField] private PlayerMovement pMove; // Reference to the PlayerMovement script
    [SerializeField] private Animator anim;       // Reference to the Animator component

    // Animator parameter hashes
    private static readonly int IsJumpHash = Animator.StringToHash("isJump");
    private static readonly int IsRunHash = Animator.StringToHash("isRun");
    private static readonly int LeftMoveHash = Animator.StringToHash("leftMove");
    private static readonly int RightMoveHash = Animator.StringToHash("rightMove");
    private static readonly int IsDashHash = Animator.StringToHash("isDash");
    private static readonly int HorizontalHash = Animator.StringToHash("Horizontal");
    private static readonly int VerticalHash = Animator.StringToHash("Vertical");

    private Vector2 lastMoveInput; // Cache to store the previous movement input
    private bool wasDashing;      // Cache to track the previous dashing state

    void Start()
    {
        anim = GetComponent<Animator>();
        pMove = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
        // Update horizontal and vertical parameters only if they change
        if (pMove.moveInput != lastMoveInput)
        {
            anim.SetFloat(HorizontalHash, pMove.moveInput.x);
            anim.SetFloat(VerticalHash, pMove.moveInput.y);
            lastMoveInput = pMove.moveInput; // Update the cache
        }

        // Update jump state
        anim.SetBool(IsJumpHash, !pMove.isGrounded);

        // Update running state
        bool isRunning = Mathf.Abs(pMove.moveInput.y) > 0.1f;
        anim.SetBool(IsRunHash, isRunning);

        // Update strafing states
        anim.SetBool(RightMoveHash, pMove.moveInput.x > 0.1f);
        anim.SetBool(LeftMoveHash, pMove.moveInput.x < -0.1f);

        // Update dashing state only if it changes
        if (pMove.isDashing != wasDashing)
        {
            anim.SetBool(IsDashHash, pMove.isDashing);
            wasDashing = pMove.isDashing; // Update the cache
        }
    }
}

