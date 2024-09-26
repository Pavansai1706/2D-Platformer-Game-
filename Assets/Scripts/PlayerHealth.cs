using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private int health;
    private int maxHealth = 3;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int Health { get => health; set => health = value; }

    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private GameOverController gameOverController; // Reference to the GameOverController

    private void Start()
    {
        Health = MaxHealth;

        if (animator == null)
        {
            Debug.LogError("Animator component not assigned on the player object.");
        }

        if (gameOverController == null)
        {
            Debug.LogError("GameOverController not assigned in the Inspector.");
        }
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0)
        {
            // Call the KillPlayer method or perform other actions when player dies
            KillPlayer();
        }
        else
        {
            // Trigger the damage animation
            if (animator != null)
            {
                animator.SetTrigger("Damage");
            }
        }
    }

    public void KillPlayer()
    {
        // Implement logic to handle player death here
        Debug.Log("Player is dead");

        // Toggle the GameOverController
        if (gameOverController != null)
        {
            gameOverController.ToggleGameOverObject(true);
        }
        else
        {
            Debug.LogError("GameOverController not found. Cannot toggle game over.");
            // You may want to handle this case, such as reloading the scene or showing a message.
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the colliding object is an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Player takes 1 damage when colliding with an enemy
            TakeDamage(1);
        }
    }
}



