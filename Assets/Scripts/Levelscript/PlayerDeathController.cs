using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    public Animator playerAnimator; // Reference to the player's Animator component
    public GameObject gameOverObject; // Reference to the game over object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            Debug.Log("Player is Dead");
            // Trigger the death animation
            playerAnimator.SetTrigger("Die");

            // Deactivate the player object
            gameObject.SetActive(false);

            // Activate the game over object
            gameOverObject.SetActive(true);
        }
    }
}
