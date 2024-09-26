using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField] private Sprite emptyHeart;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Image[] hearts;

    [SerializeField] private PlayerHealth playerHealth; // Make sure to assign this in the Inspector

    private void Update()
    {
        if (playerHealth != null) // Check if playerHealth is assigned
        {
            int health = playerHealth.Health; // Accessing health using the Health property
            int maxHealth = playerHealth.MaxHealth; // Accessing maxHealth using the MaxHealth property

            for (int i = 0; i < hearts.Length; i++)
            {
                if (i < health)
                {
                    hearts[i].sprite = fullHeart;
                    hearts[i].enabled = true; // Enable hearts that should be visible
                }
                else
                {
                    hearts[i].sprite = emptyHeart;
                    hearts[i].enabled = (i < maxHealth); // Disable hearts beyond maxHealth
                }
            }
        }
    }
}




