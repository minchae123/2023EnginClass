using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputt : MonoBehaviour
{
    private UserInputAction inputAction;

    public Action<Vector2> OnMovement;
    public Action OnJump;

    private bool uiMode = false;

    private void Awake()
    {
        inputAction = new UserInputAction();
        inputAction.Player.Enable();
        inputAction.Player.Jump.performed += Jump;

        inputAction.UI.Submit.performed += UIPerformPress;

        inputAction.Player.Disable();

        inputAction.Player.Jump.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")
            .WithCancelingThrough("<keyborad>/escape")
            .OnComplete(op =>
            {
                Debug.Log(op.selectedControl.name);
                op.Dispose();
                var json = inputAction.SaveBindingOverridesAsJson();
                Debug.Log(json);

                inputAction.LoadBindingOverridesFromJson(json);

                inputAction.Player.Enable();
            })
            .OnCancel(op =>
            {
                Debug.Log("취소되었습니다");
                op.Dispose();
                inputAction.Player.Enable();
            })
            .Start();
    }

    private void UIPerformPress(InputAction.CallbackContext context)
    {
        Debug.Log("UI 상태");
    }

    private void Update()
    {
        Vector2 inputDir = inputAction.Player.Movement.ReadValue<Vector2>();
        OnMovement?.Invoke(inputDir);

        if(Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            inputAction.Disable();
            if(uiMode == false)
            {
                inputAction.UI.Enable();
            }
            else
            {
                inputAction.Player.Enable();
            }
            uiMode = !uiMode;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
