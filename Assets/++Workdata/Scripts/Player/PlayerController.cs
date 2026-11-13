using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    public static Action<Vector2> OnMoveInput;
    public static Action<bool> OnSprintInput;
    public static Action<bool> OnJumpInput;
    public static Action<bool> OnShiftInput;
    
    public static Action<bool> OnPose1Input;
    public static Action<bool> OnPose2Input;
    public static Action<bool> OnPose3Input;
    
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

    private bool comboStarted;
    
    [SerializeField]
    private Transform _playerTransform;
    
    [SerializeField]
    private SpriteRenderer PlayerSprite;
    
    private float Score;
    
    private float currentHitPoints;
    private float maxHitPoints = 4f;
    
    public List<GameObject> prefabList;
    public Camera targetCamera;
    public Transform playerTransform;
    public float spawnRadius = 5f;
    public float poseTimer = 5f;
    
    

    private void Awake()
    {
        _currentSpeed = walkingSpeed;
        _rb = GetComponent<Rigidbody2D>();
    }
    
    private void Start()
    {
        currentHitPoints = maxHitPoints;
        poseTimer = UnityEngine.Random.Range(3f, 10f);
    }

    private void OnEnable()
    {
        OnMoveInput += OnMove;
        OnSprintInput += OnSprint;
        OnJumpInput += OnJump;
        OnShiftInput += OnShift;
        OnPose1Input += OnPose1;
        OnPose2Input += OnPose2;
        OnPose3Input += OnPose3;
    }

    private void OnDisable()
    {
        OnMoveInput -= OnMove;
        OnSprintInput -= OnSprint;
        OnJumpInput -= OnJump;
        OnShiftInput -= OnShift;
    }

    private void FixedUpdate()
    {
        poseTimer -= Time.deltaTime;
        
        Vector2 targetVelocity = _moveInput * _currentSpeed;
        Vector2 currentVelocity = _rb.linearVelocity;
        
        if(_moveInput != Vector2.zero)
            _rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);
        else
            _rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * deceleration);
        
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
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Point1") && gameObject.tag == "Combo1")
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            Score++;
            print(Score);
            acceleration += 1f;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Point2") && gameObject.tag == "Combo2")
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            Score++;
            print(Score);
            acceleration += 1f;
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("Point3") && gameObject.tag == "Combo3")
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            Score++;
            print(Score);
            acceleration += 1f;
            Destroy(collision.gameObject);
        }
        
        if (collision.CompareTag("Obstacle"))
        {
            currentHitPoints -= 1; 
            print(currentHitPoints);
            acceleration = 1f;
        }

        if (currentHitPoints <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void EnterPoseMode()
    {
        poseTimer = UnityEngine.Random.Range(1f, 10f);
        print("in Pose Mode");
        
       // _rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        
           int prefabIndex = UnityEngine.Random.Range(0, 3);
           GameObject selectedPrefab = prefabList[prefabIndex];
        
           Vector2 randomPoint2D = UnityEngine.Random.insideUnitCircle * spawnRadius;
           Vector3 spawnPosition = playerTransform.position + new Vector3(randomPoint2D.x, 0f, randomPoint2D.y);
        
           Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
    }
    
    void Update()
    {
        // Checks if an invisible circle at the feet overlaps with the Ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, checkRadius, groundLayer);
        
        if (poseTimer <= 0)
        {
            EnterPoseMode();
        }
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

    private void OnPose1(bool value)
    {
        if (comboStarted)
        {
            transform.Rotate(0f, 0f, 90f, Space.World);
            gameObject.tag = "Combo1";
            PlayerSprite.color = Color.yellow;
        }
    }

    private void OnPose2(bool value)
    {
        if (comboStarted)
        {
            transform.Rotate(0f, 0f, 90f, Space.World);
            gameObject.tag = "Combo2";
            PlayerSprite.color = Color.red;
        }
    }
    
    private void OnPose3(bool value)
    {
        if (comboStarted)
        {
            transform.Rotate(0f, 0f, 90f, Space.World);
            gameObject.tag = "Combo3";
            PlayerSprite.color = Color.blue;
        }
    }
    private void OnShift(bool value)
    {
        comboStarted = value;
        print(value);
        gameObject.tag = "Player";
        PlayerSprite.color = Color.white;
    }
}    

