using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Core.Define;

public class AgentInput : MonoBehaviour
{
    public event Action<Vector3> OnMovementKeyPress = null;
    public event Action OnAttackKeyPress = null;
    public event Action OnRollingKeyPress = null; // �Ѹ�Ű

    [SerializeField] private LayerMask whatIsGround;

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
        OnMovementKeyPress?.Invoke(new Vector3(horizontal, 0, vertical));
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
}
