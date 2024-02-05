using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUIHandler 
{
    private List<Image> heartUI;
    private int imageIndex;
    public PlayerHealthUIHandler(List<Image> _heartUI)
    {
        heartUI = _heartUI;
        EventService.Instance.OnPlayerDamaged.AddEventListener(DecreaseHearts);
        imageIndex = heartUI.Count - 1;
    }

    public void Cleanup()
    {
        EventService.Instance.OnPlayerDamaged.RemoveEventListener(DecreaseHearts);
        heartUI.Clear();
    }
    private void DecreaseHearts(float damage)
    {
        damage /= 100;
        Image heart = heartUI[imageIndex];
        heart.fillAmount -= damage;

        if (heart.fillAmount <= 0)
        {
            heart.fillAmount = 0;
            imageIndex--;
        }

        if(imageIndex < 0)
        {
            EventService.Instance.OnPlayerDied.InvokeEvent();
            DisableHearts();
        }

    }

    private void DisableHearts()
    {
        foreach (var heart in heartUI)
        {
            heart.gameObject.SetActive(false);
        }
    }
}
