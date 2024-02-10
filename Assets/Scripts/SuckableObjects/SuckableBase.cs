using System;
using UnityEngine;

public abstract class SuckableBase
{
    public static Action<int, Usability> RenderPlatformBoxUsability;
    public static Action<int, Usability> RenderRoboGunProjectileUsability; //Usability events to render global usability of pooled projectiles
    public abstract void EnterState(SuckableObjectStateManager obj);

    public virtual void UpdateState(SuckableObjectStateManager obj) { }

    public virtual void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision) { }

    public virtual void OnTriggerEnter(SuckableObjectStateManager obj, Collider2D collision) { }
}
