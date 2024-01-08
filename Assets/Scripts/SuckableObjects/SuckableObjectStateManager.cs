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
        ChooseStartingState();

        if (currentState != null)
            currentState.EnterState(this);
    }

    private void OnDisable()
    {
        currentState = null;
        isSucked = false;
    }

    private void OnEnable()
    {
        ChooseStartingState();
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
                currentState = null;    //It is not ideal to enable the shooting state from inside the object itself, as that would tie in to 
                break;                  //handling script execution order leading to null reference exception issues, so I've decided to set the
                                        //current state as null and enable shooting by the launcher object.
            default:
                currentState = idle;
                break;
        }
    }

    private void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdateState(this);
        else
            Debug.Log("NULL STATE SCRIPT EXECUTING");
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
