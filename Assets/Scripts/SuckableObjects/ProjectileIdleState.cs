using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileIdleState : SuckableBase
{
    private Rigidbody2D rb;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        rb = obj.GetRigidbody();
        rb.constraints = RigidbodyConstraints2D.None; //allows free 2D physics

        obj.ToggleHitbox(false); //disabling the hitbox so that damage wont get registered
        obj.enabled = false; //disabling the state manager so that empty Update() calls wont happen
    }
}
