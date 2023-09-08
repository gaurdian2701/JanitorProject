using System;
using UnityEngine;

public abstract class SuckableBase
{
    public static Action<GameObject> objectSucked;
    public abstract void EnterState(SuckableObjectStateManager obj);

    public abstract void UpdateState(SuckableObjectStateManager obj);

    public abstract void OnCollisionEnter(SuckableObjectStateManager obj,Collision2D collision);
}
