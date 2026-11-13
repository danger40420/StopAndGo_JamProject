using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    public PlayerController playerController;
    
    private InputSystem_Actions _inputActions;
    private InputAction _moveAction;

    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
    }

    private void OnEnable()
    {
        InputEnable();
        SubscribeActions();
    }

    private void OnDisable()
    {
        InputDisable();
        UnsubscribeActions();
    }

    private void SubscribeActions()
    {
        _moveAction.performed += MoveInput;
        _moveAction.canceled += MoveInput;
    }
    
    private void UnsubscribeActions()
    {
        _moveAction.performed -= MoveInput;
        _moveAction.canceled -= MoveInput;
    }
    
    public void InputEnable()
    {
        _inputActions.Enable();
    }

    public void InputDisable()
    {
        _inputActions.Disable();
    }
    
    private void MoveInput(InputAction.CallbackContext context)
    {
        PlayerController.OnMoveInput?.Invoke(_moveAction.ReadValue<Vector2>());
    }    
}
