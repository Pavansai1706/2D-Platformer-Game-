using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 3;
    
    void Start()
    {
        health = maxHealth;
    }
     public void TakeDamage(int amount)
    {
        health -= amount;
        if(health <= 0)
        {
            Debug.Log("Player is dead");
        }
    }
  
}
