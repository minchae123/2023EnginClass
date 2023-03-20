using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f, gravity = -9.8f;

    private CharacterController characterController;

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity => movementVelocity; // ?????
    private float verticalVelocity; // ??¨ù??

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        movementVelocity = new Vector3(horizontal, 0, vertical);
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity.Normalize();

        movementVelocity *= moveSpeed * Time.deltaTime;

        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;
        if (movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity); // ?? ???? ???? ???
        }

    }

    public void StopImmediately()
    {
        movementVelocity = Vector3.zero;
    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement(); // ?¡À???? ??? ???

        if (characterController.isGrounded == false)
        {
            verticalVelocity = gravity * Time.fixedDeltaTime;
        }
        else
        {
            // 0.3?? ?????? ??
            verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);
    }
}
