using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float gravityMultiplier = 1f;
    [SerializeField] private float jumpPower = 3f;

    private CharacterController characterController;
    public bool IsGround => characterController.isGrounded;

    private Vector2 inputDirection;
    private Vector3 movementVelocity;
    public Vector3 MovmentVelocity;
    private float verticalVelocity;

    public bool ActiveMove { get; set; } = true;

    private PlayerInput playerInput;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        playerInput = GetComponent<PlayerInput>();
        playerInput.OnMovement += SetPlayerMovement;
        playerInput.OnJump += Jump;
    }

    private void Aim()
    {

    }

    private void Jump()
    {
        if(!IsGround) return;
        verticalVelocity += jumpPower;
    }

    private void SetPlayerMovement(Vector2 dir)
    {
        inputDirection = dir;
    }

    private void CalculateMovement()
    {
        movementVelocity = new Vector3(inputDirection.x, 0, inputDirection.y) * (moveSpeed * Time.fixedDeltaTime);

        /*if(movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity);
        }*/
    }

    private void ApplyGravity()
    {
        if(IsGround && verticalVelocity < 0)
        {
            verticalVelocity = -1f;
        }
        else
        {
            verticalVelocity += gravity * gravityMultiplier * Time.fixedDeltaTime;
        }
        movementVelocity.y = verticalVelocity;
    }

    private void Move()
    {
        characterController.Move(movementVelocity);
    }

    private void FixedUpdate()
    {
        if(ActiveMove)
        {
            CalculateMovement();
        }
        ApplyGravity();
        Move();
    }

    public void StopImmediately()
    {
        movementVelocity = Vector3.zero;
    }

    public void SetMovement(Vector3 value)
    {
        verticalVelocity = value.y;
        movementVelocity = new Vector3(value.x, 0, value.z);
    }
}
