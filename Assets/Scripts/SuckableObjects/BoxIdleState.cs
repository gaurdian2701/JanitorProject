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

    public override void UpdateState(SuckableObjectStateManager obj)
    {

    }

    public override void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision)
    {
        if((collision.gameObject.layer == LayerMask.NameToLayer("Player")))
            Debug.Log("BOX: " + collision.gameObject.GetComponent<PlayerSuckCollision>().ignore);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Player") && !collision.gameObject.GetComponent<PlayerSuckCollision>().ignore)
        {
            obj.sucker = collision.gameObject;
            obj.SwitchState(obj.sucked);
        }
    }
}
