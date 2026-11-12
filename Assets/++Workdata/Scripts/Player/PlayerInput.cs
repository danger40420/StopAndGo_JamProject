using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    private InputAction _moveAction;
    
    private InputAction _interactAction;
    
    
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _interactAction = _inputActions.Player.Interact;
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
        
      //  _interactAction.performed += InteractInput;
    }
    
    private void UnsubscribeActions()
    {
        _moveAction.performed -= MoveInput;
        _moveAction.canceled -= MoveInput;
        
      //  _interactAction.performed -= InteractInput;
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
    
   // private void InteractInput(InputAction.CallbackContext context)
   // {
   //     PlayerInteraction.OnInteract?.Invoke();
   // }
}

