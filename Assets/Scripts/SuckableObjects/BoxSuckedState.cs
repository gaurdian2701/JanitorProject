using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BoxSuckedState : SuckableBase
{
    private Vector3 sizDecreaseRate = new Vector3(2.5f, 2.5f, 0f);
    private bool boxSucked;
    private Transform suckPos;

    public override void EnterState(SuckableObjectStateManager obj)
    {
        obj.transform.localScale = obj.originalSize;
        boxSucked = false;
        suckPos = obj.sucker.GetAttachments(PlayerChildren.Children.SuckPosition);
    }


    public override void UpdateState(SuckableObjectStateManager obj)
    {
        if (boxSucked)
            return;

        if (obj.transform.localScale.x > 0.1f)
            obj.transform.localScale -= sizDecreaseRate * Time.deltaTime;

        else
        {
            objectSucked.Invoke(obj.gameObject);
            boxSucked = true;
        }

        obj.transform.Rotate(obj.transform.forward, 25f);
        
        obj.transform.position = Vector3.LerpUnclamped(obj.transform.position, suckPos.position, 1f);
    }

    public override void OnCollisionEnter(SuckableObjectStateManager obj, Collision2D collision)
    {
        
    }
}
