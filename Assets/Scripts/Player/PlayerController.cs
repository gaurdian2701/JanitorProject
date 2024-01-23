using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;
using System.Threading.Tasks;

public class PlayerController : MonoBehaviour
{
    [Header("MOVEMENT")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float verticalJumpPower;
    [SerializeField] private float extraJumpFactor;
    [SerializeField] private float midairForwardAccelaration;
    [SerializeField] private float midairDecelaration;

    [Header("HEALTH")]
    [SerializeField] private float healthAmount;

    private Animator animator;
    private CapsuleCollider2D playerCollider;


    private float currentForwardPower = 1f;
    private Rigidbody2D rb;
    private float moveDirection;
    private float currentMoveSpeed;
    private PlayerShootController shootController;
    private bool isJumping;
    private bool isAttacking;

    private PlayerState playerState;
    RaycastHit2D hit;

    public PlayerHealth playerHealth;

    private void Awake()
    {
        playerState = PlayerState.idle;
        currentMoveSpeed = moveSpeed;
        isJumping = false;
        isAttacking = false;

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        shootController = GetComponent<PlayerShootController>();
        animator = GetComponent<Animator>();

        playerHealth = new PlayerHealth(healthAmount, GetComponent<SpriteRenderer>());
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection * currentMoveSpeed * currentForwardPower, rb.velocity.y);
        animator.SetFloat("MoveSpeed", Mathf.Abs(moveDirection));
        CheckForFall();
        animator.SetInteger("PlayerState", (int)playerState);
    }

    #region Player Movement
    public void PlayerMove(InputAction.CallbackContext context) //Left and Right Movement
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

    public void PlayerJump(InputAction.CallbackContext context) //Jumping
    {
        playerState = PlayerState.jumping;

        if (isJumping)
            return;

        if (context.performed && IsGrounded())
            ExecuteJump(verticalJumpPower);

        else if (context.performed && !IsGrounded() && !shootController.SuckedObjectsListEmpty())
        {
            ExecuteJump(verticalJumpPower * extraJumpFactor);
            shootController.ReleaseSuckedObjects();
        }
    }

    public IEnumerator JumpingToggle()
    {
        isJumping = true;
        yield return new WaitForSecondsRealtime(0.2f);
        isJumping = false;
    }

    private void ExecuteJump(float jumpPower)
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpPower);
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

    private void CheckForFall() //Function to check grounded state in order to trigger proper animation transitions between jumping and falling
    {
        if (!IsGrounded() && rb.velocity.y != 0f)
        {
            playerState = rb.velocity.y < 0f ? PlayerState.falling : PlayerState.jumping;
            DecreaseForwardPower();
        }

        else if (IsGrounded())
            playerState = PlayerState.idle;
    }

    private async void HaltPlayer() //Function to stop player movement during attacking or sucking
    {
        currentMoveSpeed = 0f;
        await Task.Delay(300);
        currentMoveSpeed = moveSpeed;
    }

    private bool IsGrounded()
    {
        hit = Physics2D.CircleCast(playerCollider.bounds.center, 0.3f, -transform.up, 1.3f, LayerMask.GetMask("Ground"));
        if (hit)
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
            HaltPlayer();
    }

    public void PlayerSuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerSucked");
        }

        if (IsGrounded())
            HaltPlayer();
    }

    public bool CheckIfAttacking()
    {
        return isAttacking;
    }

    public void ToggleAttackState()
    {
        isAttacking = !isAttacking;
    }
    #endregion
}
