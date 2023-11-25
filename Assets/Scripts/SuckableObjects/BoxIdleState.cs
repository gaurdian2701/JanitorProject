using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxIdleState : SuckableBase
{
    private Rigidbody2D rb;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        rb = obj.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.None;
    }
}
