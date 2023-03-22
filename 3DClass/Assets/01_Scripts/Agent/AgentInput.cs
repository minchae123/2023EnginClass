using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.Define;

public class AgentInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnAttackKeyPress = null;
    public event Action OnRollingKeyPress = null; // ·Ñ¸µÅ°

    [SerializeField] private LayerMask whatIsGround;

    private Vector3 directionInput;

    private void Update()
    {
        UpdateMovement();
        UpdateAttackInput();
        UpdateRollingInput();
    }

    private void UpdateRollingInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            OnRollingKeyPress?.Invoke();
        }
    }

    private void UpdateAttackInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnAttackKeyPress?.Invoke();
        }
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        directionInput = new Vector3(horizontal, 0, vertical);
        OnMovementKeyPress?.Invoke(directionInput);
    }

    public Vector3 GetMouseWorldPosition()
    {
        Ray ray = MainCam.ScreenPointToRay (Input.mousePosition);

        RaycastHit hit;
        bool result = Physics.Raycast(ray, out hit, MainCam.farClipPlane, whatIsGround);
        if (result)
        {
            return hit.point;
        }
        else
        {
            return Vector3.zero;
        }
    }

    public Vector3 GetCurrentInputDirection()
    {
        Vector3 dir45 = Quaternion.Euler(0, -45, 0) * directionInput.normalized;
        return dir45;
    }
}
