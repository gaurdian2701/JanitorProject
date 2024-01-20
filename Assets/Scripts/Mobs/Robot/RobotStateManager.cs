using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotStateManager : MonoBehaviour
{
    [SerializeField] private GameObject roboGun;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private LayerMask playerMask;


    RobotBaseState currentState;

    public RobotIdleState idle = new RobotIdleState();
    public RobotAlertState alert = new RobotAlertState();   

    private float moveSpeed = 2f;
    private float currentSpeed;

    private Transform playerTransform;

    private Animator animator;
    private BoxCollider2D robotCollider;
    private SpriteRenderer sprite;
    private ObjectShooter roboShoot;

    public RobotHealth robotHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        robotCollider = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();

        roboGun.transform.rotation = Quaternion.identity;

        robotHealth = new RobotHealth(this);
        roboShoot = GetComponent<ObjectShooter>();
    }
    private void Start()
    {
        currentState = idle;
        currentSpeed = moveSpeed;
        currentState.EnterState(this);
    }

    private void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        currentState.OnTriggerEnter(collision, this);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        currentState.OnTriggerExit(collision, this);
    }
    public void SwitchState(RobotBaseState robot)
    {
        currentState = robot;
        currentState.EnterState(this);
    }

    public void SetRobotShooting(bool _activeState) { roboShoot.enabled = _activeState; }
    public Transform GetPlayerTransform() { return playerTransform; }

    public void SetPlayerTransform(Transform pos) { playerTransform = pos; }

    public LayerMask GetGroundMask() { return groundMask; }

    public LayerMask GetPlayerMask() { return playerMask; } 
    public GameObject GetRoboGun() { return roboGun; }
    public float GetCurrentSpeed(){ return currentSpeed;}

    public SpriteRenderer GetSpriteRenderer() { return sprite; }

    public Animator GetAnimator() { return animator; }

    public BoxCollider2D GetRobotCollider() { return robotCollider; }
    public void SetCurrentSpeed(float speed) { currentSpeed = speed; }
    public void RestoreSpeed() => currentSpeed = moveSpeed;
}