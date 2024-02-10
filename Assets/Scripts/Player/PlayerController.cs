using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using Unity.VisualScripting;
using System.Text;
using System.Threading.Tasks;
using System;
using UnityEngine.U2D;
using UnityEngine.UI;
using TMPro;

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
    private SpriteRenderer spriteRenderer;


    private float currentForwardPower = 1f;
    private Rigidbody2D rb;
    private float moveDirection;
    private float currentMoveSpeed;
    private bool isJumping;
    private bool isAttacking;
    private bool isPaused;
    private Color spriteOriginalColor;

    private PlayerState playerState;
    RaycastHit2D hit;

    public PlayerHealth playerHealth;
    private PlayerShootController shootController;
    private PlayerStatusHandler playerStatusHandler;

    private void Awake()
    {
        playerState = PlayerState.idle;
        currentMoveSpeed = moveSpeed;
        isJumping = false;
        isAttacking = false;
        isPaused = false;

        rb = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        shootController = GetComponent<PlayerShootController>();
        animator = GetComponent<Animator>();

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteOriginalColor = spriteRenderer.material.GetColor("_Color");

        playerHealth = new PlayerHealth(this);
        playerStatusHandler = new PlayerStatusHandler(this, shootController);

        EventService.Instance.OnPlayerDied.AddEventListener(HandlePlayerDeath);
    }

    private void OnDisable()
    {
        EventService.Instance.OnPlayerDied.RemoveEventListener(HandlePlayerDeath);
    }

    private void HandlePlayerDeath()
    {
        playerStatusHandler.Disable();
        this.gameObject.SetActive(false);
    }

    public float GetMoveSpeed() { return moveSpeed; }
    public void SetMoveSpeed(float _speed) => currentMoveSpeed = _speed;

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

    public void PauseGame(InputAction.CallbackContext context) //Pause Game
    {
        if (context.performed)
        {
            isPaused = isPaused ? false : true;
            EventService.Instance.OnGamePaused.InvokeEvent(isPaused);
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
            InitiateCoroutine(PLayerCoroutineType.HaltPlayer);
    }

    public void InitiateCoroutine(PLayerCoroutineType routine)
    {
        switch (routine)
        {
            case PLayerCoroutineType.HaltPlayer:
                HaltPlayer(300); break;

            case PLayerCoroutineType.InitiateDamageFlash:
                FlashColor(150, Color.red); break;

            default: break;
        }
    }

    public void PlayerSuck(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            animator.SetTrigger("PlayerSucked");
        }

        if (IsGrounded())
            InitiateCoroutine(PLayerCoroutineType.HaltPlayer);
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

    #region Coroutines

    private async void FlashColor(int time, Color color) //Flashes the player sprite in a certain color
    {
        spriteRenderer.material.color = color;
        await Task.Delay(time);
        spriteRenderer.material.color = spriteOriginalColor;
    }

    private async void HaltPlayer(int time) //Function to stop player movement while shooting or sucking
    {
        currentMoveSpeed = 0f;
        await Task.Delay(time);
        currentMoveSpeed = moveSpeed;
    }
    #endregion
}
