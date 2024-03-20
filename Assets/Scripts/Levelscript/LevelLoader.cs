using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

[RequireComponent(typeof(Button))]
public class LevelLoader : MonoBehaviour
{
    private Button button; // Changed access modifier to private

    [SerializeField]
    private string levelName; // Changed access modifier to private

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick); // Changed method name to follow C# conventions
    }

    private void OnClick() // Changed access modifier to private
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName); // Accessed levelName with private modifier
        switch (levelStatus)
        {
            case LevelStatus.Locked:
                Debug.Log("Can't play this level till you unlock it");
                break;

            case LevelStatus.Unlocked:
            case LevelStatus.Completed: // Combined cases since they do the same thing
                SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene(levelName); // Accessed levelName with private modifier
                break;
        }
    }
}


