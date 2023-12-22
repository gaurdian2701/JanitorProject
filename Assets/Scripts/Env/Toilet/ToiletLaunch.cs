using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToiletLaunch : MonoBehaviour
{
    [SerializeField] private Animator launcherAnimator;

    private PlayerController player;
    private void Awake()
    {
        launcherAnimator.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.GetComponent<PlayerController>();
        StatusManager.ApplyStatus.Invoke(Status.CannotShoot);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(player && player.CheckIfAttacking())
        {
            launcherAnimator.enabled=true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            StatusManager.ApplyStatus.Invoke(Status.CanShoot);
            player = null;
        }
    }
}
