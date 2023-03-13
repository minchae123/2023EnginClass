using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f, gravity = -9.8f;

    private CharacterController characterController;

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity => movementVelocity; // ���ӵ�
    private float verticalVelocity; // �߷¼ӵ�

    private AgentAnimator animator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetMovementVelocity(Vector3 value)
    {
        movementVelocity = value;

    }

    private void CalculatePlayerMovement()
    {
        animator?.SetSpeed(MovementVelocity.sqrMagnitude);
        movementVelocity.Normalize();

        movementVelocity *= moveSpeed * Time.deltaTime;
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        if(movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity); // �� ���� ���� �ϱ�
        }

    }

    public void StopImmediately()
    {
        movementVelocity = Vector3.zero;
        animator?.SetSpeed(MovementVelocity.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement(); // �÷��̾� �̼� ���

        if(characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            // 0.3�� �ϵ��ڵ� ��
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }
        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);

        animator?.SetAirbone(characterController.isGrounded == false);
    }
}
