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
            StartCoroutine(IgnoreCollisions(collision.gameObject));
        }
    }

    private IEnumerator IgnoreCollisions(GameObject obj) 
    {
        ignore = true;
        yield return new WaitForSecondsRealtime(0.5f);
        ignore = false;
    }
}
