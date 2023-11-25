using System;
using UnityEngine;

public abstract class SuckableBase
{
    public static Action<GameObject> objectSucked;
    public abstract void EnterState(SuckableObjectStateManager obj);

    public virtual void UpdateState(SuckableObjectStateManager obj) { }

    public virtual void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision) { }
}
