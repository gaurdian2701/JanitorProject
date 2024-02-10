using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [Header("PLAYER HEALTH")]
    [SerializeField] private List<Image> heartUI;
    private PlayerHealthUIHandler playerHealthUI;

    [Header("SUCKED OBJECTS")]
    [SerializeField] private List<Image> suckedObjects;
    private SuckedObjectsUI suckedObjectsUI;

    [Header("UI")]
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private GameObject LevelCompleteScreen;
    [SerializeField] private GameObject PauseScreen;
    private void Awake()
    {
        if (GameOverScreen && LevelCompleteScreen && PauseScreen)
        {
            GameOverScreen.SetActive(false);
            LevelCompleteScreen.SetActive(false);
            PauseScreen.SetActive(false);
        }

        EventService.Instance.OnPlayerDied.AddEventListener(ShowGameOverScreen);
        EventService.Instance.OnGamePaused.AddEventListener(HandlePauseEvent);
        EventService.Instance.OnLevelComplete.AddEventListener(HandleLevelCompleteEvent);

        playerHealthUI = new PlayerHealthUIHandler(heartUI);
        suckedObjectsUI = new SuckedObjectsUI(suckedObjects); //Instantiate UI scripts for the player hearts and sucked objects
    }

    private void OnDestroy()
    {
        playerHealthUI.Cleanup();
        suckedObjectsUI.Cleanup();

        heartUI.Clear();
        suckedObjects.Clear();

        EventService.Instance.OnPlayerDied.RemoveEventListener(ShowGameOverScreen);
        EventService.Instance.OnGamePaused.RemoveEventListener(HandlePauseEvent);
        EventService.Instance.OnLevelComplete.RemoveEventListener(HandleLevelCompleteEvent);
    }

    private void HandlePauseEvent(bool pauseState) //Pause game
    {
        PauseScreen.SetActive(pauseState);
    }

    private void HandleLevelCompleteEvent() //Level complete
    {
        LevelCompleteScreen.SetActive(true);
    }

    private void ShowGameOverScreen() //Game over
    {
        GameOverScreen.SetActive(true);
    }
}
