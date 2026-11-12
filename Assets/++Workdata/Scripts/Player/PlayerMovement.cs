using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] PhysicsMaterial2D airMaterial2D;

    [SerializeField] float movementSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] private float coyoteTime;

    [SerializeField] private float normalGravity = 2;
    [SerializeField] private float fallingGravity = 4;

    [SerializeField] private Animator animator;

    [SerializeField] private GameManager gameManager;

    private float movement;
    private bool isMoving;
    private float timeSinceGrounded;
    private bool coyoteJump;
    private bool grounded;
    private bool doubleJumpAvailable;
    private bool jumpButtonIsPressed;

    [Header("Ground Check Settings")] public Transform groundCheckPoint;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;

    public bool isGrounded;

    void FixedUpdate()
    {
        //movement
        {
            rb2D.linearVelocityX = movement * movementSpeed * Time.deltaTime;
        }

        if (movement < 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (movement > 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        //Faster Fall
        if (rb2D.linearVelocityY < 0)
        {
            rb2D.gravityScale = fallingGravity;
        }
        else if (rb2D.linearVelocityY > 0 && !jumpButtonIsPressed)
        {
            rb2D.gravityScale = fallingGravity;
        }
        else
        {
            rb2D.gravityScale = normalGravity;
        }
    }

    void Update()
    {
        // Checks if an invisible circle at the feet overlaps with the Ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer);
    }

    // Optional: Draws a red circle in the Scene view so you can see the check area
    void OnDrawGizmosSelected()
    {
        if (groundCheckPoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckPoint.position, checkRadius);
    }

    //movementDirection
    public void Move(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>().x;

        if (movement != 0)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isIdle", false);
        }
        else if (movement == 0)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", true);
        }

        if (grounded)
        {
            animator.SetBool("isGrounded", true);
            animator.SetBool("isJumping", false);
        }

        // if (grounded == false)
        // {
        //     Debug.Log("falling");
        //     animator.SetBool("isJumping", true);
        // }

    }

    public void Restart(InputAction.CallbackContext context)
    {
        gameManager.Restart();
    }

    //Jump
    public void Jump(InputAction.CallbackContext context)
    {
        //how long is jump button pressed
        if (context.phase == InputActionPhase.Started)
        {
            doubleJumpAvailable = false;
            jumpButtonIsPressed = true;
            animator.SetBool("isGrounded", false);
            animator.SetBool("isJumping", true);
            animator.SetBool("isRunning", false);
            animator.SetBool("isIdle", false);
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            jumpButtonIsPressed = false;
            animator.SetBool("isJumping", false);
        }

        if (context.phase == InputActionPhase.Canceled && movement != 0)
        {
            animator.SetBool("isRunning", true);
        }

        if (context.performed && grounded)
        {
            animator.SetBool("isGrounded", true);
        }

        if (context.performed)
        {

            if (grounded || doubleJumpAvailable || coyoteJump)
            {
                rb2D.linearVelocityY = jumpForce;

                if (!grounded && !coyoteJump)
                {
                    doubleJumpAvailable = false;
                }

                else if (!grounded && coyoteJump)
                {
                    coyoteJump = false;
                }
            }
        }
    }
}