using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8, gravity = -9.8f;

    private CharacterController characterController;

    private Vector3 movementVelocity;
    private float verticalVelocity;
    private Vector3 inputVelovity;

    private AgentAnimator agentAnimator;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetInputVelocity(Vector3 value)
    {
        inputVelovity = value;
        agentAnimator.SetDirection(new Vector2(value.x, value.z));
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity = inputVelovity.normalized * moveSpeed * Time.fixedDeltaTime;
        agentAnimator?.SetMoving(movementVelocity.sqrMagnitude > 0);
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement();

        verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;

        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);
    }
}
