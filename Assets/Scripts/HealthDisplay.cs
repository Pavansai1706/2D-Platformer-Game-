using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{
    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth; // Make sure to assign this in the Inspector

    void Update()
    {
        if (playerHealth != null) // Check if playerHealth is assigned
        {
            int health = playerHealth.health;
            int maxHealth = playerHealth.maxHealth;

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
        else
        {
            Debug.LogWarning("PlayerHealth component is not assigned.");
        }
    }
}
