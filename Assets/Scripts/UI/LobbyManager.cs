using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    [SerializeField] private GameObject LevelList;
    [SerializeField] private GameObject Menu;
    private void Start()
    {
        HideLevelList();
    }
    public void ShowLevelList()
    {
        LevelList.SetActive(true);
        Menu.SetActive(false);
    }

    public void HideLevelList()
    {
        LevelList.SetActive(false);
        Menu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
