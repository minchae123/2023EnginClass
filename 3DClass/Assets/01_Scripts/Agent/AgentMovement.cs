using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsActiveMove = true; // 키보드로 이동하냐 아니냐

    [SerializeField] private float moveSpeed = 8f, gravity = -9.8f;

    private CharacterController characterController;

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity => movementVelocity; // 평면속도
    private float verticalVelocity; // 중력속도

    private AgentAnimator animator;

    private Vector3 inputVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        animator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetMovementVelocity(Vector3 value)
    {
        inputVelocity = value;
        movementVelocity = value;
    }

    public void CalculatePlayerMovement()
    {
        animator?.SetSpeed(inputVelocity.sqrMagnitude);
        inputVelocity.Normalize();

        movementVelocity = inputVelocity * (moveSpeed * Time.fixedDeltaTime);
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        if (movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity); // 갈 방향 보게 하기
        }

    }

    public void StopImmediately()
    {
        movementVelocity = Vector3.zero;
        animator?.SetSpeed(MovementVelocity.sqrMagnitude);
    }

    public void SetRotation(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FixedUpdate()
    {
        if(IsActiveMove)
            CalculatePlayerMovement(); // 플레이어 이속 계산 (키보드 입력시에만 45도 계산)

        if(characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            // 0.3은 하드코딩 값
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }
        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);

        animator?.SetAirbone(characterController.isGrounded == false);
    }
}
