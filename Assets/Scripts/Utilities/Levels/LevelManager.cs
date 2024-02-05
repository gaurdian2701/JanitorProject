using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : GenericMonoSingleton<LevelManager>
{
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        if (GetLevelStatus("Level1") == LevelStatus.Locked)
            SetLevelStatus("Level1", LevelStatus.Unlocked);
    }

    public LevelStatus GetLevelStatus(string levelName)
    {
        LevelStatus levelStatus = (LevelStatus)PlayerPrefs.GetInt(levelName, 0);
        return levelStatus;
    }

    public void SetLevelStatus(string levelName, LevelStatus status)
    {
        PlayerPrefs.SetInt(levelName, (int)status);
    }

    public void MarkLevelComplete(string levelName)
    {
        SetLevelStatus(levelName, LevelStatus.Completed);
        int nextSceneBuildIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SetLevelStatus("Level" + nextSceneBuildIndex, LevelStatus.Unlocked);
    }
}
