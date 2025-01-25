using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class freeze_bullet_manager : MonoBehaviour
{
    [SerializeField] private float freezeDuration ; // Time to keep the scripts disabled
    private List<PlayerMovement> disabledPlayerMovements = new List<PlayerMovement>(); // Track disabled PlayerMovement scripts
    private List<PlayerAttack> disabledPlayerAttacks = new List<PlayerAttack>(); // Track disabled PlayerAttack scripts
    private List<float> movementTimers = new List<float>(); // Timers for PlayerMovement scripts
    private List<float> attackTimers = new List<float>(); // Timers for PlayerAttack scripts

    private void Update()
    {
        // Update timers for PlayerMovement scripts
        for (int i = disabledPlayerMovements.Count - 1; i >= 0; i--)
        {
            movementTimers[i] -= Time.deltaTime;

            if (movementTimers[i] <= 0f)
            {
                disabledPlayerMovements[i].enabled = true;
                Debug.Log("PlayerMovement re-enabled!");
                disabledPlayerMovements.RemoveAt(i);
                movementTimers.RemoveAt(i);
            }
        }

        // Update timers for PlayerAttack scripts
        for (int i = disabledPlayerAttacks.Count - 1; i >= 0; i--)
        {
            attackTimers[i] -= Time.deltaTime;

            if (attackTimers[i] <= 0f)
            {
                disabledPlayerAttacks[i].enabled = true;
                Debug.Log("PlayerAttack re-enabled!");
                disabledPlayerAttacks.RemoveAt(i);
                attackTimers.RemoveAt(i);
            }
        }
    }

    /// <summary>
    /// Disables a PlayerMovement script and starts a timer to re-enable it.
    /// </summary>
    public void FreezePlayerMovement(PlayerMovement movement)
    {
        if (movement != null && movement.enabled)
        {
            movement.enabled = false;
            disabledPlayerMovements.Add(movement);
            movementTimers.Add(freezeDuration);
            Debug.Log("PlayerMovement disabled for " + freezeDuration + " seconds.");
        }
    }

    /// <summary>
    /// Disables a PlayerAttack script and starts a timer to re-enable it.
    /// </summary>
    public void FreezePlayerAttack(PlayerAttack attack)
    {
        if (attack != null && attack.enabled)
        {
            attack.enabled = false;
            disabledPlayerAttacks.Add(attack);
            attackTimers.Add(freezeDuration);
            Debug.Log("PlayerAttack disabled for " + freezeDuration + " seconds.");
        }
    }

    /// <summary>
    /// Freezes both PlayerMovement and PlayerAttack scripts on the given GameObject.
    /// </summary>
    public void FreezePlayer(GameObject player)
    {
        PlayerMovement movement = player.GetComponent<PlayerMovement>();
        PlayerAttack attack = player.GetComponent<PlayerAttack>();

        FreezePlayerMovement(movement);
        FreezePlayerAttack(attack);
    }



}
