using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float verticalJumpPower;
    [SerializeField] private float midairForwardAccelaration;
    [SerializeField] private float midairDecelaration;
    [SerializeField] private CapsuleCollider2D playerCollider;

    private float currentForwardPower = 1f;
    private Rigidbody2D rb;
    private float moveDirection;
    private float currentMoveSpeed;
    private SuckedObjectsController suckerController;
    private bool isJumping;

    private enum PlayerState
    {
        idle,
        walking,
        falling,
        jumping,
        attacking,
        sucking
    };

    private PlayerState playerState;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();     
        playerState = PlayerState.idle;
        currentMoveSpeed = moveSpeed;
        suckerController = GetComponent<SuckedObjectsController>();
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        RaycastHit2D rayhit = Physics2D.Raycast(playerCollider.bounds.center, -transform.up, 1.3f, LayerMask.GetMask("Platform"));
        Gizmos.DrawRay(playerCollider.bounds.center, -transform.up);

        if (rayhit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(playerCollider.bounds.center, -transform.up);
        }
    }

    public void PlayerMove(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<float>();

        Vector3 newRotation;

        if (moveDirection < 0f)
        {
            newRotation = new Vector3(0, 180, 0);
            transform.eulerAngles = newRotation;
            ResetForwardPower();
        }

        else if (moveDirection > 0f)
        {
            newRotation = new Vector3(0, 0, 0);
            transform.eulerAngles = newRotation;
        }
    }

    public void PlayerAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerAttacked");
        }

        if(IsGrounded())
            StartCoroutine(HaltPlayer());
    }

    public void PlayerSuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerSucked");
        }

        if(IsGrounded())
            StartCoroutine(HaltPlayer());
    }

    public void PlayerJump(InputAction.CallbackContext context)
    {
        playerState = PlayerState.jumping;

        if (isJumping)
            return;

        if (context.performed && IsGrounded())
            ExecuteJump();

        else if (context.performed && !IsGrounded() && !suckerController.SuckedObjectsListEmpty())
            ExecuteJump();
    }

    public void isJumpingToggle() => isJumping = !isJumping;

    private void ExecuteJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalJumpPower);
        currentForwardPower = midairForwardAccelaration;
    }

    void ResetAnimationTriggers()
    {
        animator.Rebind();
        animator.ResetTrigger("PlayerAttacked");
        animator.ResetTrigger("PlayerSucked");
    }

    void DecreaseForwardPower()
    {
        if (currentForwardPower <= 1f)
            ResetForwardPower();

        else
            currentForwardPower -= midairDecelaration;
    }
    void ResetForwardPower() => currentForwardPower = 1f;

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * currentMoveSpeed * currentForwardPower, rb.velocity.y);
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDirection));
        CheckForFall();
    }

    void CheckForFall()
    {
        if (!IsGrounded() && rb.velocity.y < -0.1f)
        {
            playerState = PlayerState.falling;
            DecreaseForwardPower();
        }

        else if (IsGrounded())
            playerState = PlayerState.idle;

        animator.SetInteger("PlayerState", (int)playerState);
    }

    private IEnumerator HaltPlayer()
    {
        currentMoveSpeed = 0f;
        yield return new WaitForSecondsRealtime(0.5f);
        currentMoveSpeed = moveSpeed;
    }

    bool IsGrounded()
    {
        if (Physics2D.Raycast(playerCollider.bounds.center, -transform.up, 1.3f, LayerMask.GetMask("Platform")))
            return true;

        return false;

    }
}
