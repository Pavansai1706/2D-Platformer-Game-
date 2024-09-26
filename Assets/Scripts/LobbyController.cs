using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private Button buttonPlay;
    [SerializeField] private GameObject levelSelection;

    private void Awake()
    {
        if (buttonPlay != null)
        {
            buttonPlay.onClick.AddListener(PlayGame);
        }
    }

    private void PlayGame()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.Play(Sounds.ButtonClick);
        }

        if (levelSelection != null)
        {
            levelSelection.SetActive(true);
        }
    }
}
