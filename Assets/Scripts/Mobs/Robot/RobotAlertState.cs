using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAlertState : RobotBaseState
{
    private GameObject roboGun;
    public override void EnterState(RobotStateManager robot)
    {
        robot.GetAnimator().SetTrigger("PrepareGun");
        roboGun = robot.GetRoboGun();
    }

    public override void UpdateState(RobotStateManager robot)
    {
        AimGun(robot.GetPlayerTransform(), robot);
    }

    private void AimGun(Transform target, RobotStateManager robot)
    {
        Vector2 aimVector = target.position - robot.transform.position;
        float angle = Vector2.SignedAngle(Vector2.right, aimVector);

        if (angle > 90)
            Debug.Log("flip");
        else
            Debug.Log("unflip"); //RoboGun flipping logic

        roboGun.transform.eulerAngles = new Vector3(0, 0f, angle);
    }

    public override void OnTriggerExit(Collider2D collision, RobotStateManager robot)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            robot.GetAnimator().SetTrigger("HideGun");
            robot.SetPlayerTransform(null);
            robot.SwitchState(robot.idle);
        }
    }
}