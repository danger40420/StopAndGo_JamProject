using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Action<Vector2> OnMoveInput;
    public static Action<bool> OnSprintInput;
    
    [SerializeField]
    private float walkingSpeed = 5f;
    
    [SerializeField]
    private float runningSpeed = 10f;
    
    [SerializeField]
    private float acceleration = 10f;
    
    [SerializeField]
    private float deceleration = 10f;
    
    private Rigidbody2D rb;
    
    private Vector2 _moveInput;
    public Vector2 MoveInput => _moveInput;
    
    private float _currentSpeed;
    
    private void Awake()
    {
        _currentSpeed = walkingSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        OnMoveInput += OnMove;
        OnSprintInput += OnSprint;
    }
    
    private void OnDisable()
    {
        OnMoveInput -= OnMove;
        OnSprintInput -= OnSprint;
    }
    
    private void FixedUpdate()
    {
        Vector2 targetVelocity = _moveInput * _currentSpeed;
        Vector2 currentVelocity = rb.linearVelocity;
        
        if(_moveInput != Vector2.zero)
            rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration);
        else
            rb.linearVelocity = Vector2.Lerp(currentVelocity, targetVelocity, Time.deltaTime * deceleration);
        
        transform.eulerAngles = new Vector3(0f, _moveInput.x < 0 ? 180 : _moveInput.x > 0 ? 0 : transform.eulerAngles.y, 0f);

        if (_moveInput != Vector2.zero)
        {
            Vector3 vector3 = Vector3.left * _moveInput.x + Vector3.down * _moveInput.y;
        }
    }
    
    private void OnMove(Vector2 moveInput)
    {
        _moveInput = moveInput;
    }
    
    private void OnSprint(bool value)
    {
        _currentSpeed = value ? runningSpeed : walkingSpeed;
    }
}
