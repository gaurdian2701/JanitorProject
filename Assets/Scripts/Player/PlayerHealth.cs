using System;
using System.Threading.Tasks;
using UnityEngine;


public class PlayerHealth : IDamageable
{
    private SpriteRenderer sprite;
    private Color spriteOriginalColor;
    public static Action<float> PlayerDamaged;

    public PlayerHealth(float _health, SpriteRenderer _sprite) 
    {
        sprite = _sprite;
        spriteOriginalColor = sprite.material.GetColor("_Color");
    }

    public void TakeDamage(float _damage)
    {
        FlashRed();
        PlayerDamaged?.Invoke(_damage);
    }
    private async void FlashRed()
    {
        sprite.material.color = Color.red;
        await Task.Delay(150);
        sprite.material.color = spriteOriginalColor;
    }
}
