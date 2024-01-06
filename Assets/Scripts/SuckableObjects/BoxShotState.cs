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
        shootPos = obj.launcher.GetShootPos();
        direction = shootPos.right;
        obj.transform.position = shootPos.position;

        obj.transform.parent = null;
        obj.transform.localScale = obj.originalSize;
        obj.transform.rotation = Quaternion.identity;

        rb = obj.GetComponent<Rigidbody2D>();
        rb.constraints = shootPos.transform.rotation.z == 0f ? RigidbodyConstraints2D.FreezePositionY : RigidbodyConstraints2D.FreezePositionX;

        obj.launcher = null;
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        rb.velocity = obj.shootSpeed * direction;
    }

    public override void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision)
    {
        switch(obj.projectileType)
        {
            case ProjectileType.Pooled:
                if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    SuckableBase.RenderUsability(obj.usabilityIndex, Usability.Usable);
                    obj.gameObject.SetActive(false);
                }
                break;

            case ProjectileType.NotPooled:
                obj.SwitchState(obj.idle);
                break;

            default: break;
        }
    }
}
