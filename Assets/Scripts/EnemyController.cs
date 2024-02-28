using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float moveDistance = 10f; // Distance to move forth and back
    public int maxHealth = 3; // Maximum health of the player

    private int currentHealth; // Current health of the player
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingForward = true;
    private Vector3 originalScale;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.right * moveDistance;
        originalScale = transform.localScale;
        currentHealth = maxHealth; // Initialize current health to max health
    }

    void Update()
    {
        MoveEnemy();
    }

    void MoveEnemy()
    {
        // Move the enemy back and forth
        if (movingForward)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            if (transform.position == targetPosition)
            {
                movingForward = false;
                // Flip the enemy's scale to change direction
                transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
            if (transform.position == startPosition)
            {
                movingForward = true;
                // Flip the enemy's scale back to original direction
                transform.localScale = originalScale;
            }
        }
    }

    // Handle collisions with player
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Reduce player's health
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        // Decrease health
        currentHealth--;

        // Check if player is dead
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Update UI or perform any other action to reflect health change
            Debug.Log("Player health: " + currentHealth);
        }
    }

    void Die()
    {
        // Perform death actions here, such as restarting the level
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
