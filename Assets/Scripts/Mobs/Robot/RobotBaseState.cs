using UnityEngine;

public abstract class RobotBaseState
{
    public abstract void EnterState(RobotStateManager robot);

    public abstract void UpdateState(RobotStateManager robot);

    public virtual void OnTriggerEnter(Collider2D collision, RobotStateManager robot) { }

    public virtual void OnTriggerExit(Collider2D collision, RobotStateManager robot) { }
}