using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSuckedState : SuckableBase
{
    private bool projectileSucked;
    private Transform suckPos;
    private Vector3 shrinkRate;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        if (obj.GetProjectilePooledType() == ProjectilePooledType.Pooled)
            HandlePooledProjectileUsability(obj);

        obj.transform.localScale = obj.GetOriginalSize();
        projectileSucked = false;
        suckPos = obj.GetSuckPosition();

        shrinkRate = obj.GetShrinkRate();
    }

    private void HandlePooledProjectileUsability(SuckableObjectStateManager obj)
    {
        switch (obj.GetProjectileType())
        {
            case ProjectileType.PlatformBox:
                RenderPlatformBoxUsability.Invoke(obj.GetUsabilityIndex(), Usability.Unusable);
                break;

            case ProjectileType.RoboGunProjectile:
                RenderRoboGunProjectileUsability.Invoke(obj.GetUsabilityIndex(), Usability.Unusable);
                break;

            default:
                break;
        }
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        if (projectileSucked)
            return;

        obj.transform.Rotate(obj.transform.forward, 25f);
        obj.transform.position = Vector3.Lerp(obj.transform.position, suckPos.position, 1f);

        if (obj.transform.localScale.x > 0.1f)
            obj.transform.localScale -= shrinkRate * Time.deltaTime;

        else
        {
            projectileSucked = true;
            suckPos = null;
            ObjectSucked?.Invoke(obj.gameObject);
        }
    }
}
