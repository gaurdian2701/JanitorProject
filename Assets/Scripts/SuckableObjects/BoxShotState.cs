using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShotState : SuckableBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private Transform shootPos;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        shootPos = obj.sucker.GetAttachments(PlayerChildren.Children.ShootPosition);
        direction = shootPos.right;
        obj.transform.position = shootPos.position;

        obj.transform.parent = null;
        obj.transform.localScale = obj.originalSize;

        rb = obj.GetComponent<Rigidbody2D>();
        rb.constraints = shootPos.transform.rotation.z < 0f ? RigidbodyConstraints2D.FreezePositionX : RigidbodyConstraints2D.FreezePositionY;
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
