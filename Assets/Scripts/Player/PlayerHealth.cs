using System;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerHealth : IDamageable
{
    private float health;
    private float currentHealth;
    private SpriteRenderer sprite;
    private Color spriteOriginalColor;

    public static Action PlayerDead;

    public PlayerHealth(float _health, SpriteRenderer _sprite) 
    {
        health = _health;
        currentHealth = health;
        sprite = _sprite;
        spriteOriginalColor = sprite.material.GetColor("_Color");
    }

    public void TakeDamage(float _damage)
    {
        FlashRed();
        currentHealth -= _damage;

        if(currentHealth <= 0)
        {
            Debug.Log("PLAYER DEAD");
            currentHealth = 0;
            PlayerDead?.Invoke();
        }
    }
    private async void FlashRed()
    {
        sprite.material.color = Color.red;
        await Task.Delay(150);
        sprite.material.color = spriteOriginalColor;
    }
}
