using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    public static Action PlayerFell;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>())
            PlayerFell?.Invoke();
    }
}
