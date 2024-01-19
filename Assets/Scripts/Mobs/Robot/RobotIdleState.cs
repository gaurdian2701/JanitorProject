using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotIdleState : RobotBaseState
{
    private float direction = 0f;
    private Vector3 position;
    private BoxCollider2D robotCollider;

    private RaycastHit2D rayForward;
    private RaycastHit2D rayBottom;
    public override void EnterState(RobotStateManager robot)
    {
        robotCollider = robot.GetRobotCollider();
    }

    public override void UpdateState(RobotStateManager robot)
    {
        position = robot.transform.position;
        position.x += robot.transform.forward.z * robot.GetCurrentSpeed() * Time.deltaTime;
        robot.transform.position = position;

         rayBottom = Physics2D.Raycast(robotCollider.bounds.center, 
            new Vector2(robot.transform.forward.z, -0.5f), 2.5f, robot.groundMask);

         rayForward = Physics2D.Raycast(robotCollider.bounds.center, robot.transform.right, 1f, robot.groundMask);

        if (!rayBottom || rayForward)
        {
            if (direction == 180f)
                direction = 0f;
            else
                direction = 180f;

            robot.transform.eulerAngles = new Vector3(0f, direction, 0f);
        }
    }

    public override void OnTriggerEnter(Collider2D collision, RobotStateManager robot)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
            robot.SwitchState(robot.alert);

        robot.SetPlayerTransform(collision.transform);
    }
}