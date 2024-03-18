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
            

            // Ensure that LevelManager.Instance is not null before accessing it
            if (LevelManager.Instance != null)
            {
                LevelManager.Instance.MarkCurrentLevelComplete();
            }

            // Load the next level
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        // Get the index of the current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // Load the next scene if available
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
           
            // You can handle what to do if there's no next scene, maybe load a specific scene or go back to the main menu.
        }
    }
}

