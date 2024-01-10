using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusHandler : MonoBehaviour
{
    private ShootController shootController;
    private void Awake()
    {
        shootController = GetComponent<ShootController>();
        StatusManager.ApplyStatus += StatusHandler;
    }

    private void OnDestroy()
    {
        StatusManager.ApplyStatus -= StatusHandler;
    }

    private void StatusHandler(Status status)
    {
        switch (status)
        {
            case Status.CannotShoot:
                shootController.SetShootCondition(false);
                break;

            case Status.CanShoot:
                shootController.SetShootCondition(true);
                break;

            default: 
                break;
        }
    }
}
