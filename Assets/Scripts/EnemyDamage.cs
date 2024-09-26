using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Animator playerAnimator; // Reference to the player's Animator component
    public GameOverController gameOverController; // Reference to the GameOverController
    public int damage = 1;

    private int damageCount = 0;
    private bool isDead = false;
    private bool isIdleScheduled = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null && !isDead)
        {
            playerHealth.TakeDamage(damage);
            if (damageCount < 2)
            {
                Debug.Log("Player is damaged");
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





