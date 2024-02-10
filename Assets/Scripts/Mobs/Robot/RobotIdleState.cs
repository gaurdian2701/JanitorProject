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
        robotCollider = robot.GetRobotCollider(); //collider used for alerting the robot when player enters trigger zone
    }

    public override void UpdateState(RobotStateManager robot)
    {
        position = robot.transform.position;
        position.x += robot.transform.forward.z * robot.GetCurrentSpeed() * Time.deltaTime;
        robot.transform.position = position;

        //raycast to check is theres no ground so that the robot doesn't fall off ledges
        rayBottom = Physics2D.Raycast(robotCollider.bounds.center,
           new Vector2(robot.transform.forward.z, -0.5f), 3.5f, robot.GetGroundMask()); 

        //raycast to check for walls
        rayForward = Physics2D.Raycast(robotCollider.bounds.center, robot.transform.right, 2f, robot.GetGroundMask());

        if (!rayBottom || rayForward) //turns the opposite side if either of the rays return the spcified condition
        {
            if (direction == 180f)
                direction = 0f;
            else
                direction = 180f;

            robot.transform.eulerAngles = new Vector3(0f, direction, 0f);
        }
    }

    public override void OnTriggerEnter(Collider2D collision, RobotStateManager robot) //switch to alert state when player enters trigger zone
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            robot.SetPlayerTransform(collision.transform);
            robot.SwitchState(robot.alert);
        }
    }
}