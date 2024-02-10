using System;
using System.Collections;
using UnityEngine;


public class PlayerHealth : IDamageable
{
    private PlayerController controller;

    public PlayerHealth(PlayerController _controller) 
    {
        controller = _controller;
    }

    public void TakeDamage(float _damage)
    {
        EventService.Instance.OnPlayerDamaged.InvokeEvent(_damage);
        controller.InitiateCoroutine(PLayerCoroutineType.InitiateDamageFlash);
    }
}
