using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputActions;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _interactAction;
    private InputAction _shiftAction;
    private InputAction _pose1Action;
    private InputAction _pose2Action;
    private InputAction _pose3Action;
    
    
    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _moveAction = _inputActions.Player.Move;
        _jumpAction = _inputActions.Player.Jump;
        _interactAction = _inputActions.Player.Interact;
        _shiftAction = _inputActions.Player.StartCombo;
        _pose1Action = _inputActions.Player.Pose1;
        _pose2Action = _inputActions.Player.Pose2;
        _pose3Action = _inputActions.Player.Pose3;
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
        _jumpAction.performed += JumpInput;
        _jumpAction.canceled += JumpInput;
        _shiftAction.performed += ShiftInput;
        _shiftAction.canceled += ShiftInput;
        _pose1Action.performed += Pose1Input;
        _pose1Action.canceled += Pose1Input;
        _pose2Action.performed += Pose2Input;
        _pose2Action.canceled += Pose2Input;
        _pose3Action.performed += Pose3Input;
        _pose3Action.canceled += Pose3Input;
        

        //  _interactAction.performed += InteractInput;
    }
    
    private void UnsubscribeActions()
    {
        _moveAction.performed -= MoveInput;
        _moveAction.canceled -= MoveInput;
        _jumpAction.performed -= JumpInput;
        _jumpAction.canceled -= JumpInput;
        _shiftAction.performed -= ShiftInput;
        _shiftAction.canceled -= ShiftInput;
        _pose1Action.performed -= Pose1Input;
        _pose1Action.canceled -= Pose1Input;
        _pose2Action.performed -= Pose2Input;
        _pose2Action.canceled -= Pose2Input;
        _pose3Action.performed -= Pose3Input;
        _pose3Action.canceled -= Pose3Input;
        
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

    private void JumpInput(InputAction.CallbackContext context)
    {
        PlayerController.OnJumpInput?.Invoke(_jumpAction.triggered);
    }

    private void ShiftInput(InputAction.CallbackContext context)
    {
        PlayerController.OnShiftInput?.Invoke(_shiftAction.triggered);
    }

    private void Pose1Input(InputAction.CallbackContext context)
    {
        PlayerController.OnPose1Input?.Invoke(_pose1Action.triggered);
    }
    
    private void Pose2Input(InputAction.CallbackContext context)
    {
        PlayerController.OnPose2Input?.Invoke(_pose2Action.triggered);
    }
    
    private void Pose3Input(InputAction.CallbackContext context)
    {
        PlayerController.OnPose3Input?.Invoke(_pose3Action.triggered);
    }
    
   // private void InteractInput(InputAction.CallbackContext context)
   // {
   //     PlayerInteraction.OnInteract?.Invoke();
   // }
}

