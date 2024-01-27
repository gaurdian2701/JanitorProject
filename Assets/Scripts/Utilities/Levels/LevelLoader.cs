using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static Action LevelComplete;

    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject LevelCompleteScreen;

    private int maxScenes = 3;

    private void Awake()
    {
        KillZone.PlayerFell += ShowGameOverScreen;
        PlayerHealthHandler.PlayerDied += ShowGameOverScreen;

        if (GameOverScreen && LevelCompleteScreen)
        {
            GameOverScreen.SetActive(false);
            LevelCompleteScreen.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        KillZone.PlayerFell -= ShowGameOverScreen;
        PlayerHealthHandler.PlayerDied -= ShowGameOverScreen;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
        {
            LevelManager.Instance.MarkLevelComplete(SceneManager.GetActiveScene().name);
            LevelCompleteScreen.SetActive(true);
            LevelComplete.Invoke();
        }
    }

    private void ShowGameOverScreen()
    {
        GameOverScreen.SetActive(true);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadLobbyScene() => SceneManager.LoadScene("Lobby");

    public void LoadLevel(string levelName)
    {
        LevelStatus levelStatus = LevelManager.Instance.GetLevelStatus(levelName);

        switch (levelStatus)
        {
            case LevelStatus.Locked:
                break;

            case LevelStatus.Unlocked:
                SceneManager.LoadScene(levelName);
                break;

            case LevelStatus.Completed:
                SceneManager.LoadScene(levelName);
                break;

            default:
                break;
        }
    }

    public void LoadNextLevel()
    {
        int index = SceneManager.GetActiveScene().buildIndex + 1;
        string nextLevel = "Level" + index;

        if (index > maxScenes)
            LoadLobbyScene();

        else
            LoadLevel(nextLevel);

    }
}
