using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    public GameObject gameOverObject; // Reference to the game over object

    public void ToggleGameOverObject(bool isActive)
    {
        // Activate or deactivate the game over object based on the parameter
        gameOverObject.SetActive(isActive);
    }
    public Button buttonRestart;

    public static object Instance { get; internal set; }

    private void Awake()
    {
        buttonRestart.onClick.AddListener(ReloadLevel);
    }
    public void PlayerDied()
    {
        gameObject.SetActive(true);

    }
  
    private void ReloadLevel()
    {
     
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }

}
