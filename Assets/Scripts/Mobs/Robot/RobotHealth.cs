using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class RobotHealth : IDamageable
{
    private SpriteRenderer sprite;
    private Color spriteOriginalColor;
    public RobotHealth(RobotStateManager robot)
    {
        sprite = robot.GetSpriteRenderer();
        spriteOriginalColor = sprite.material.GetColor("_Color");
    }
    public void TakeDamage(float damage)
    {
        //Health reduce logic
        FlashRed();
    }

    private async void FlashRed()
    {
        sprite.material.color = Color.red;
        await Task.Delay(150);
        sprite.material.color = spriteOriginalColor;
    }
}
