using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelOverController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with the player
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            Debug.Log("Level Finished by the player");

            // Ensure that LevelManager.Instance is not null before accessing it
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.MarkCurrentLevelComplete();
            }
          
        }
    }
}

