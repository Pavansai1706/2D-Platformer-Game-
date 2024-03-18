using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private Animator playerAnimator; // Reference to the player's Animator component
    private GameOverController gameOverController; // Reference to the GameOverController
    private int damage = 1;

    private int damageCount = 0;
    private bool isDead = false;
    private bool isIdleScheduled = false;

    private void Start()
    {
        playerHealth = FindObjectOfType<PlayerHealth>(); // Assuming PlayerHealth is a component attached to the player
        playerAnimator = GetComponent<Animator>(); // Assuming the animator is attached to the enemy
        gameOverController = FindObjectOfType<GameOverController>(); // Assuming GameOverController is a singleton or attached to a persistent object
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && !isDead)
        {
            playerHealth.TakeDamage(damage);
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
    public void ScheduleIdleAnimation()
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
