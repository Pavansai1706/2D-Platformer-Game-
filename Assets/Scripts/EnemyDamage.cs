using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private Animator playerAnimator; // Reference to the player's Animator component
    private GameOverController gameOverController; // Reference to the GameOverController

    private int damageCount = 0;
    private bool isDead = false;
    private bool isIdleScheduled = false;

    private void Start()
    {
        playerAnimator = GetComponent<Animator>(); // Assuming the animator is attached to the enemy
        gameOverController = FindObjectOfType<GameOverController>(); // Assuming GameOverController is a singleton or attached to a persistent object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isDead)
        {
            if (damageCount < 2)
            {
                // Trigger the damage animation
                playerAnimator.SetTrigger("Damage");
                damageCount++;
            }
            else
            {
                // Trigger the death animation
                playerAnimator.SetTrigger("Die");
                isDead = true;

                // Toggle the game over object if it's set in the Inspector
                if (gameOverController != null)
                {
                    gameOverController.ToggleGameOverObject(true);
                }
            }
        }
    }

    // Function to be called from the animation event
    private void ScheduleIdleAnimation()
    {
        isIdleScheduled = true;
    }

    private void Update()
    {
        if (isIdleScheduled && !isDead)
        {
            // Play idle animation
            playerAnimator.SetTrigger("Idle");
            isIdleScheduled = false;
        }
    }
}

