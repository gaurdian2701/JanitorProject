using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxShotState : SuckableBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private GameObject shootPos;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        shootPos = obj.transform.parent.Find("ShootPosition").gameObject;
        direction = shootPos.transform.right;
        obj.transform.position = shootPos.transform.position;

        obj.transform.parent = null;
        obj.transform.localScale = obj.originalSize;

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
