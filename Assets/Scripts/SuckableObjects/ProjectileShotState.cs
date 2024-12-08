using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ProjectileShotState : SuckableBase
{
    private Rigidbody2D rb;
    private Vector2 direction;
    private Transform shootPos;

    private GameObject collidedObject;
    private int layer;
    
    public override void EnterState(SuckableObjectStateManager obj)
    {
        shootPos = obj.GetShootPosition();
        direction = shootPos.right;
        obj.transform.position = shootPos.position;

        obj.transform.parent = null;
        obj.transform.localScale = obj.GetOriginalSize();
        obj.transform.rotation = Quaternion.identity;
        obj.transform.right = direction;

        rb = obj.GetComponent<Rigidbody2D>();

        obj.SetLauncher(null);
        obj.ToggleHitbox(true);
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        rb.linearVelocity = obj.GetShootSpeed() * direction;
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
                            SuckableBase.RenderRoboGunProjectileUsability(obj.GetUsabilityIndex(), Usability.Usable); //Rendering usability for corresponding projectiles
                            break;
                    }
                    obj.gameObject.SetActive(false); //Deactivating object on collision which makes it part of the unused pool once again
                }
                break;

            case ProjectilePooledType.NotPooled:
                obj.SwitchState(obj.idle); // Non-pooled objects simply switch to idle state
                break;

            default: break;
        }
    }

    public override void OnTriggerEnter(SuckableObjectStateManager obj, Collider2D collision) //Logic for handling hitbox trigger collisions
    {
        collidedObject = collision.gameObject;
        layer = collidedObject.layer;

        if (layer == LayerMask.NameToLayer("EnemyHurtBox"))
            collidedObject.transform.parent.GetComponent<RobotStateManager>().robotHealth?.TakeDamage(obj.GetProjectileDamage()); //Deal damage to enemy

        else if (layer == LayerMask.NameToLayer("PlayerHurtBox"))
        {
            collidedObject.transform.parent.GetComponent<PlayerController>().playerHealth?.TakeDamage(obj.GetProjectileDamage()); //Deal damage to player
            EventService.Instance.OnApplyStatus.InvokeEvent(obj.GetStatusApplied()); //Apply status effect
        }

        if(obj.GetProjectilePooledType() == ProjectilePooledType.Pooled)
            obj.gameObject.SetActive(false); //If the object is pooled then return it to the pool
    }
}
