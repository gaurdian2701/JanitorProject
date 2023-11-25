using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
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

    private void Awake()
    {
        playerState = PlayerState.idle;
        currentMoveSpeed = moveSpeed;
        isJumping = false;

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        suckerController = GetComponent<SuckedObjectsController>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * currentMoveSpeed * currentForwardPower, rb.velocity.y);
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDirection));
        CheckForFall();
        animator.SetInteger("PlayerState", (int)playerState);
    }

    #region Input and Miscallaneous functions
    public void DisableInput()
    {
        if (IsGrounded())
            playerInput.DeactivateInput();
    }

    public void EnableInput()
    {
        playerInput.ActivateInput();
    }
    public Transform GetAttachments(PlayerChildren.Children child)
    {
        return transform.GetChild((int)child);
    }
    #endregion

    #region Player Movement
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

    public void PlayerJump(InputAction.CallbackContext context)
    {
        playerState = PlayerState.jumping;

        if (isJumping)
            return;

        if (context.performed && IsGrounded())
            ExecuteJump();

        else if (context.performed && !IsGrounded() && !suckerController.SuckedObjectsListEmpty())
        {
            ExecuteJump();
            suckerController.ReleaseSuckedObjects();
        }
    }

    public IEnumerator JumpingToggle()
    {
        isJumping = true;
        yield return new WaitForSecondsRealtime(0.2f);
        isJumping = false;
    }

    private void ExecuteJump()
    {
        rb.velocity = new Vector2(rb.velocity.x, verticalJumpPower);
        currentForwardPower = midairForwardAccelaration;
    }

    private void DecreaseForwardPower()
    {
        if (currentForwardPower <= 1f)
            ResetForwardPower();

        else
            currentForwardPower -= midairDecelaration;
    }
    private void ResetForwardPower() => currentForwardPower = 1f;

    private void CheckForFall()
    {
        if (!IsGrounded() && rb.velocity.y != 0f)
        {
            playerState = rb.velocity.y < 0f ? PlayerState.falling : PlayerState.jumping;
            DecreaseForwardPower();
        }

        else if (IsGrounded())
            playerState = PlayerState.idle;
    }

    private IEnumerator HaltPlayer()
    {
        currentMoveSpeed = 0f;
        yield return new WaitForSecondsRealtime(0.3f);
        currentMoveSpeed = moveSpeed;
    }

    private bool IsGrounded()
    {
        if (Physics2D.Raycast(playerCollider.bounds.center, -transform.up, 1.3f, LayerMask.GetMask("Ground")))
            return true;

        return false;

    }
    #endregion

    #region Player Attack
    public void PlayerAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerAttacked");
        }

        if (IsGrounded())
            StartCoroutine(HaltPlayer());
    }

    public void PlayerSuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerSucked");
        }

        if (IsGrounded())
            StartCoroutine(HaltPlayer());
    }
    #endregion
}
