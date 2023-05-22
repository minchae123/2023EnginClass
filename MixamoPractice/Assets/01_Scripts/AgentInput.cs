using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentInput : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsGround;

    public UnityEvent<Vector3> OnMovementKeyPress = null;

    private void Update()
    {
        UpdateMoveInput();
    }

    private void UpdateMoveInput()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        OnMovementKeyPress?.Invoke(new Vector3(h, 0, v));
    }
}
