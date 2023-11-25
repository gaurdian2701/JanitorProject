using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObjectStateManager : MonoBehaviour
{
    private SuckableBase currentState;

    public BoxIdleState idle = new BoxIdleState();
    public BoxSuckedState sucked = new BoxSuckedState();
    public BoxShotState shot = new BoxShotState();
    public PlayerController sucker;

    public Vector3 originalSize = new Vector3(1f, 1f, 1f);
    public float shootSpeed;
    public bool isSucked;

    private void Awake()
    {
        isSucked = false;
    }
    private void Start()
    {
        currentState = idle;
        currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(SuckableBase state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void SwitchToShoot()
    {
        SwitchState(shot);
        isSucked = false;
    }
    public void SwitchToSuck()
    {
        SwitchState(sucked);
        isSucked = true;
    }
}
