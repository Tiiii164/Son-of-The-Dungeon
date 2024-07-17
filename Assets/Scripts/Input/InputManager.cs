﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;
    public static Vector2 PointerPosition;
    public static bool Attack;
    public static bool Dash;
    public static bool Interact;
    public static InputManager instance;
    private InputAction _moveAction;
    private InputAction _leftClickAction;
    private InputAction _pointerPositionAction;
    private InputAction _leftShiftAction;
    private InputAction _rightClickAction;
    private InputAction _interactAction;
    private PlayerInput _playerInput;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }
        //gọi ra để ref tới thằng input actions
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];
        _leftClickAction = _playerInput.actions["Attack"];
        _pointerPositionAction = _playerInput.actions["PointerPosition"];
        _leftShiftAction = _playerInput.actions["Dash"];
        _rightClickAction = _playerInput.actions["Dash"];
        _interactAction = _playerInput.actions["Interact"];
    }
    private void Update()
    {
        //nhận giá trị liên tục cho mấy thằng khác sài
        Movement = _moveAction.ReadValue<Vector2>();
        PointerPosition = _pointerPositionAction.ReadValue<Vector2>();
        Attack = _leftClickAction.WasPressedThisFrame();
        //Attack = _leftClickAction.ReadValue<float>() > 0.5f;
        Dash = _leftShiftAction.WasPressedThisFrame();
        Dash = _rightClickAction.WasPressedThisFrame();

        Interact = _interactAction.WasPressedThisFrame();
        
    }
    public void DisableDashAction()
    {
        _leftShiftAction.Disable();
        _rightClickAction.Disable();
    }
    public void EnableDashAction()
    {
        _leftShiftAction.Enable();
        _rightClickAction.Enable();
    }
}