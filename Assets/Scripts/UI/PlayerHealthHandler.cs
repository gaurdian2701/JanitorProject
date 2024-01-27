using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthHandler : MonoBehaviour 
{
    [SerializeField] private List<Image> HeartUI;

    public static Action PlayerDied;
    private int imageIndex;
    private void Awake()
    {
        PlayerHealth.PlayerDamaged += DecreaseHearts;
        imageIndex = HeartUI.Count - 1;
    }

    private void OnDestroy()
    {
        PlayerHealth.PlayerDamaged -= DecreaseHearts;
    }
    private void DecreaseHearts(float damage)
    {
        damage /= 100;
        Image heart = HeartUI[imageIndex];
        heart.fillAmount -= damage;

        if (heart.fillAmount <= 0)
        {
            heart.fillAmount = 0;
            imageIndex--;
        }

        if(imageIndex < 0)
        {
            PlayerDied?.Invoke();
            DisableHearts();
        }

    }

    private void DisableHearts()
    {
        foreach (var heart in HeartUI)
        {
            heart.gameObject.SetActive(false);
        }
    }
}
