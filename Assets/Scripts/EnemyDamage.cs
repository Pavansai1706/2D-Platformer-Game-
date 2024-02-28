using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamage : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public int damage = 1;
    public Animator playerAnimator; // Reference to the player's Animator component
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
                // Reload the level after a delay
                StartCoroutine(RestartLevelAfterDelay(1f)); // Adjust the delay time as needed
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

    IEnumerator RestartLevelAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}



