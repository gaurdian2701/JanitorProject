using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSuckCollision : MonoBehaviour
{
    public bool ignore;
    private PlayerController controller;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        ignore = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SuckableObjectStateManager obj = collision.gameObject.GetComponent<SuckableObjectStateManager>();

        if (obj && !obj.isSucked && !ignore)
        {
            obj.sucker = controller;
            obj.SwitchToSuck();
            ignore = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suckable"))
        {
            ignore = false;
        }
    }
}