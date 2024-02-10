using System;
using System.Threading.Tasks;
using UnityEngine;

public class RobotHealth : IDamageable
{
    private SpriteRenderer sprite;
    private Color spriteOriginalColor;
    private float health;
    private float currentHealth;
    private RobotStateManager robot;
    public RobotHealth(SpriteRenderer _sprite, float _health, RobotStateManager _robot)
    {
        sprite = _sprite;
        health = _health;
        currentHealth = _health;
        robot = _robot;
        spriteOriginalColor = sprite.material.GetColor("_Color");
    }
    public void TakeDamage(float damage)
    {
        FlashRed();
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            robot.OnDeath();
        }
    }

    private async void FlashRed()
    {
        sprite.material.color = Color.red;
        await Task.Delay(150);
        sprite.material.color = spriteOriginalColor;
    }
}
