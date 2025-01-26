using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour
{
    [SerializeField] private PlayerMovement pMove; // Reference to the PlayerMovement script
    [SerializeField] private Animator anim; // Reference to the Animator component
    [SerializeField] private float movementBlendSpeed = 0.02f;
    //private Rigidbody playerRB;
    
    //[SerializeField] private PlayerAttack PA;

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
        //playerRB = GetComponent<Rigidbody>();
        //PA = GetComponent<PlayerAttack>();
    }

    void Update()
    {
        UpdateAnimationParameters();
    }

    void UpdateAnimationParameters()
    {
            // Log moveInput.x to debug
        Debug.Log($"moveInput.x: {pMove.moveInput.x}");

        // Update horizontal and vertical parameters only if they change
        
        
            anim.SetFloat(HorizontalHash, pMove.moveInput.x);
            anim.SetFloat(VerticalHash, pMove.moveInput.y);
            //lastMoveInput = pMove.moveInput; // Update the cache
        

        // Update jump state
        anim.SetBool(IsJumpHash, !pMove.isGrounded);

        // Update running state
        //bool isRunning = Mathf.Abs(pMove.moveInput.y) > 0.1f;
        //anim.SetBool(IsRunHash, isRunning);

        // Refined strafing states
       /*  if (pMove.moveInput.x > 0.1f)
        {
            anim.SetBool(RightMoveHash, true);
            anim.SetBool(LeftMoveHash, false); // Ensure the other direction is reset
        }
        else if (pMove.moveInput.x < -0.1f)
        {
            anim.SetBool(RightMoveHash, false);
            anim.SetBool(LeftMoveHash, true); // Ensure the other direction is reset
        }
        else
        {
            anim.SetBool(RightMoveHash, false);
            anim.SetBool(LeftMoveHash, false); // Reset both if not strafing
        } */

        // Update dashing state only if it changes
        if (pMove.isDashing != wasDashing)
        {
            anim.SetBool(IsDashHash, pMove.isDashing);
            wasDashing = pMove.isDashing; // Update the cache
        }

    }


}
/*
void UpdateAnimationParameters()
{
    
}

*/

