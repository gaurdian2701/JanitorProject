using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuckableObjectStateManager : MonoBehaviour
{
    [SerializeField] private ObjectState startingState;
    [SerializeField] private float shrinkValue;
    private SuckableBase currentState;
    private Vector3 shrinkRate;

    public BoxIdleState idle = new BoxIdleState();
    public BoxSuckedState sucked = new BoxSuckedState();
    public BoxShotState shot = new BoxShotState();
    public GameObject launcher;

    public Vector3 originalSize = new Vector3(1f, 1f, 1f);
    public float shootSpeed;
    public bool isSucked;

    public ProjectileType projectileType;
    public int usabilityIndex;
    private void Awake()
    {
        isSucked = false;
        shrinkRate = new Vector3(shrinkValue, shrinkValue, 0f);
    }
    private void Start()
    {
        ChooseStartingState();
        currentState.EnterState(this);
    }

    private void ChooseStartingState()
    {
        switch (startingState)
        {
            case ObjectState.idle:
                currentState = idle;
                break;

            case ObjectState.sucked:
                currentState = sucked;
                break;

            case ObjectState.shot:
                currentState = shot;
                break;

            default:
                currentState = idle;
                break;
        }
    }

    private void FixedUpdate()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public Vector3 GetShrinkRate()
    {
        return shrinkRate;
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
