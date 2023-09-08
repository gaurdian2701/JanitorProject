using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShotState : SuckableBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        direction = obj.transform.parent.right;

        obj.transform.parent = null;

        obj.transform.localScale = obj.originalSize;
        obj.transform.position = GameObject.Find("Player").transform.Find("ShootPosition").position;

        rb = obj.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        rb.velocity = obj.shootSpeed * direction;
    }

    public override void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision)
    {
        obj.SwitchState(obj.idle);
    }
}
