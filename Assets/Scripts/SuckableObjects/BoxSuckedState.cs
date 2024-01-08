using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxSuckedState : SuckableBase
{
    private Vector3 sizDecreaseRate = new Vector3(3f, 3f, 0f);
    private bool boxSucked;
    private Transform suckPos;

    public override void EnterState(SuckableObjectStateManager obj)
    {
        if(obj.projectileType == ProjectileType.Pooled)
            RenderUsability.Invoke(obj.usabilityIndex, Usability.Unusable);

        obj.transform.localScale = obj.originalSize;
        boxSucked = false;
        suckPos = obj.launcher.GetSuckPos();
    }

    public override void UpdateState(SuckableObjectStateManager obj)
    {
        if (boxSucked)
            return;

        Debug.Log(sizDecreaseRate * Time.deltaTime);

        if (obj.transform.localScale.x > 0.1f)
            obj.transform.localScale -= sizDecreaseRate * Time.deltaTime;

        else
        {
            ObjectSucked.Invoke(obj.gameObject);
            boxSucked = true;
        }

        obj.transform.Rotate(obj.transform.forward, 25f);
        
        obj.transform.position = Vector3.Lerp(obj.transform.position, suckPos.position, 1f);
    }
}
