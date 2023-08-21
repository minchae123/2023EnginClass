using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private PlayerInputAction _inputAction;
    public PlayerInputAction InputAction => _inputAction;

    public event Action<Vector2> OnMovement;
    public Vector2 AimPosition { get; private set; }
    public event Action OnJump;
    public event Action OnFire;

    private void Awake()
    {
        _inputAction = new PlayerInputAction();

        _inputAction.Player.Enable();
        _inputAction.Player.Jump.performed += JumpHandle;
        _inputAction.Player.Fire.performed += FireHandle;
    }

    private void FireHandle(InputAction.CallbackContext context)
    {
        OnFire?.Invoke();
    }

    private void JumpHandle(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Update()
    {
        AimPosition = _inputAction.Player.Aim.ReadValue<Vector2>();

        Vector2 dir = _inputAction.Player.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(dir);
    }
}
