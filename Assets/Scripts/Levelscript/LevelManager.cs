using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance; // Changed access modifier to private
    public static LevelManager Instance { get { return instance; } }

    [SerializeField]
    private string levelName; // Changed access modifier to private

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start() // Changed access modifier to private
    {
        if (GetLevelStatus(levelName) == LevelStatus.Locked)
        {
            SetLevelStatus(levelName, LevelStatus.Unlocked);
        }
    }

    public void MarkCurrentLevelComplete() // Changed access modifier to private
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SetLevelStatus(currentScene.name, LevelStatus.Completed);

        int nextSceneIndex = currentScene.buildIndex + 1;
        Scene nextScene = SceneManager.GetSceneByBuildIndex(nextSceneIndex);
        SetLevelStatus(nextScene.name, LevelStatus.Unlocked);
    }

    public LevelStatus GetLevelStatus(string level) // Changed access modifier to private
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(level, 0);
        return levelStatus;
    }

    private void SetLevelStatus(string level, LevelStatus levelStatus) // Changed access modifier to private
    {
        PlayerPrefs.SetInt(level, (int)levelStatus);
        Debug.Log("Setting Level: " + level + " Status: " + levelStatus);
    }
}

