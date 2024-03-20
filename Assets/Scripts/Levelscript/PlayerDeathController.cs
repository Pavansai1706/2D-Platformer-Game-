using UnityEngine;

public class PlayerDeathController : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator; // Changed access modifier to private
    [SerializeField] private GameObject gameOverObject; // Changed access modifier to private

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

