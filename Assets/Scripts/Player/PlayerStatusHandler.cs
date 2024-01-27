using System.Threading.Tasks;
using UnityEngine;

public class PlayerStatusHandler
{
    private PlayerShootController shootController;
    private PlayerController playerController;

    private float playerOriginalSpeed;

    public PlayerStatusHandler(PlayerController _playerController, PlayerShootController _shootController)
    {
        shootController = _shootController;
        playerController = _playerController;
        playerOriginalSpeed = playerController.GetMoveSpeed();
        StatusManager.ApplyStatus += StatusHandler;
    }
    public void Disable()
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

            case Status.SlowDown:
                playerController.SetMoveSpeed(playerOriginalSpeed/2);
                WaitForStatusTime(status, 3000);
                break;

            default: 
                break;
        }
    }

    private void RemoveStatus(Status status)
    {
        Debug.Log("Remove Status");
        switch (status)
        {
            case Status.SlowDown:
                playerController.SetMoveSpeed(playerOriginalSpeed);
                break;

            default:
                break;
        }
    }
    private async void WaitForStatusTime(Status status, int millisecs)
    {
        await Task.Delay(millisecs);
        RemoveStatus(status);
    }
}
