using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSuckCollision : MonoBehaviour
{
    public bool ignore;

    private void Start()
    {
        ignore = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suckable") && !ignore)
        {
            ignore = true;
            Debug.Log("PLAYER: " + ignore);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Suckable"))
        {
            ignore = false;
            Debug.Log("PLAYER: " + ignore);
        }
    }
}
