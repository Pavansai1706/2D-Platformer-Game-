using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeathController : MonoBehaviour
{
    public Animator playerAnimator; // Reference to the player's Animator component
    private bool isDeathAnimationTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDeathAnimationTriggered && collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player is Dead");
            // Trigger the death animation
            playerAnimator.SetTrigger("Die");
            isDeathAnimationTriggered = true;

            // Restart the level after a delay
            StartCoroutine(RestartLevelAfterDelay(1f)); // Adjust the delay time as needed
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