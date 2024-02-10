using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : GenericMonoSingleton<LevelController>
{
    private int maxScenes = 3;

    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        EventService.Instance.OnGamePaused.AddEventListener(HandlePauseEvent);
        EventService.Instance.OnLevelComplete.AddEventListener(HandleLevelComplete);
    }

    private void OnDestroy()
    {
        EventService.Instance.OnGamePaused.RemoveEventListener(HandlePauseEvent);
        EventService.Instance.OnLevelComplete.RemoveEventListener(HandleLevelComplete);
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1.0f;
    }

    public void HandlePauseEvent(bool pauseState)
    {
        Time.timeScale = pauseState ? 0f : 1f;
    }

    private void HandleLevelComplete()
    {
        LevelManager.Instance.MarkLevelComplete(SceneManager.GetActiveScene().name);
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

        Time.timeScale = 1.0f;
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
