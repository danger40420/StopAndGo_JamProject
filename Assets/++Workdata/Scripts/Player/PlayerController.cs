using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<Vector2> OnMoveInput;
    public static Action<bool> OnSprintInput;
    public static Action<bool> OnJumpInput;
    
    [SerializeField]
    private float walkingSpeed = 5f;
    
    [SerializeField]
    private float runningSpeed = 8f;
    
    [SerializeField]
    private float acceleration = 10f;
    
    [SerializeField]
    private float deceleration = 10f;
    
    [SerializeField]
    private float jumpForce = 5f;
    
    [SerializeField]
    private float coyoteTime;

    [SerializeField]
    private float normalGravity = 2f;

    [SerializeField]
    private float fallingGravity = 4f;
    
    [Header("Ground Check Settings")] public Transform groundCheckPoint;
    public float checkRadius = 0.2f;
    public LayerMask groundLayer;
    
    public bool isGrounded;
    
    private Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;
    
    private Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;

    private bool jumpButtonIsPressed;
    
    private float _currentSpeed;

    private void Awake()
    {
        _currentSpeed = walkingSpeed;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OnMoveInput += OnMove;
        OnSprintInput += OnSprint;
        OnJumpInput += OnJump;
    }

    private void OnDisable()
    {
        OnMoveInput -= OnMove;
        OnSprintInput -= OnSprint;
        OnJumpInput -= OnJump;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity = _moveInput * _currentSpeed;
        Vector2 currentVelocity = _rb.linearVelocity;
        
        if(_moveInput != Vector2.zero)
            _rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime);
        else
            _rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime);
        
        transform.eulerAngles = new Vector3(0f, _moveInput.x < 0 ? 180 : _moveInput.x > 0 ? 0 : transform.eulerAngles.y, 0f);
        
        //Faster Fall
        if (_rb.linearVelocityY < 0)
        {
            _rb.gravityScale = fallingGravity;
        }
        else if (_rb.linearVelocityY > 0 && !jumpButtonIsPressed)
        {
            _rb.gravityScale = fallingGravity;
        }
        else
        {
            _rb.gravityScale = normalGravity;
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

    private void OnMove(Vector2 moveInput)
    {
        _moveInput.x = moveInput.x;
    }

    private void OnSprint(bool value)
    {
        _currentSpeed = value ? runningSpeed : walkingSpeed;
    }

    private void OnJump(bool value)
    {
        if (isGrounded)
        {
            _rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}    

