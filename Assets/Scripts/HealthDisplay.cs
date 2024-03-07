using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthDisplay : MonoBehaviour
{
    public int health;
    public int maxHealth; 

    public Sprite emptyHeart;
    public Sprite fullHeart;
    public Image[] hearts;

    public PlayerHealth playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        health = playerHealth.health;
        maxHealth = playerHealth.maxHealth;

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
