using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileSuckedState : SuckableBase
{
    private bool boxSucked;
    private Transform suckPos;
    private Vector3 shrinkRate;
    public override void EnterState(SuckableObjectStateManager obj)
    {
        if(obj.GetProjectilePooledType() == ProjectilePooledType.Pooled)
            RenderPlatformBoxUsability.Invoke(obj.GetUsabilityIndex(), Usability.Unusable);

        obj.transform.localScale = obj.GetOriginalSize();
        boxSucked = false;
        suckPos = obj.GetSuckPosition();

        shrinkRate = obj.GetShrinkRate();
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        if (boxSucked)
            return;

        obj.transform.Rotate(obj.transform.forward, 25f);
        obj.transform.position = Vector3.Lerp(obj.transform.position, suckPos.position, 1f);

        if (obj.transform.localScale.x > 0.1f)
            obj.transform.localScale -= shrinkRate * Time.deltaTime;

        else
        {
            ObjectSucked.Invoke(obj.gameObject);
            boxSucked = true;
            suckPos = null;
        }
    }
}
