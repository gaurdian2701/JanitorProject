using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileShotState : SuckableBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private Transform shootPos;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        shootPos = obj.GetShootPosition();
        direction = shootPos.right;
        obj.transform.position = shootPos.position;

        obj.transform.parent = null;
        obj.transform.localScale = obj.GetOriginalSize();
        obj.transform.rotation = Quaternion.identity;

        rb = obj.GetComponent<Rigidbody2D>();
        //rb.constraints = Mathf.Abs((shootPos.transform.rotation.z * 180) % 180) == 0f ? RigidbodyConstraints2D.FreezePositionY : RigidbodyConstraints2D.FreezePositionX;

        obj.SetLauncher(null);
        obj.ToggleHitbox(true);
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        rb.velocity = obj.GetShootSpeed() * direction;
    }

    public override void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision)
    {
        switch(obj.GetProjectilePooledType())
        {
            case ProjectilePooledType.Pooled:
                if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    switch(obj.GetProjectileType())
                    {
                        case ProjectileType.PlatformBox:
                            SuckableBase.RenderPlatformBoxUsability(obj.GetUsabilityIndex(), Usability.Usable);
                            break;

                        case ProjectileType.RoboGunProjectile:
                            SuckableBase.RenderRoboGunProjectileUsability(obj.GetUsabilityIndex(), Usability.Usable);
                            break;
                    }
                    obj.gameObject.SetActive(false);
                }
                break;

            case ProjectilePooledType.NotPooled:
                obj.SwitchState(obj.idle);
                break;

            default: break;
        }
    }

    public override void OnTriggerEnter(SuckableObjectStateManager obj, Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyHurtBox"))
            collision.gameObject.transform.parent.GetComponent<RobotStateManager>().robotHealth.TakeDamage(0f);
    }
}
