using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8f, gravity = -9.8f;

    private CharacterController characterController;

    private Vector3 movementVelocity;
    public Vector3 MovementVelocity => movementVelocity; // ���ӵ�
    private float verticalVelocity; // �߷¼ӵ�

    public event Action<Vector3> OnMovementKeyPress = null;


    public GameObject bullet;
    public GameObject pos;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        UpdateMovement();

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(1);
            Shoot();
        }
    }

    public void SetMovementVelocity(Vector3 value)
    {
        movementVelocity = value;
    }

    private void CalculatePlayerMovement()
    {
        movementVelocity.Normalize();

        movementVelocity *= moveSpeed * Time.deltaTime;
        movementVelocity = Quaternion.Euler(0, -45f, 0) * movementVelocity;

        if (movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movementVelocity); // �� ���� ���� �ϱ�
        }

    }

    private void FixedUpdate()
    {
        CalculatePlayerMovement(); // �÷��̾� �̼� ���

        verticalVelocity = gravity * 0.3f * Time.fixedDeltaTime;
        Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
        characterController.Move(move);

    }

    private void UpdateMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        OnMovementKeyPress?.Invoke(new Vector3(horizontal, 0, vertical));
    }

    public void Shoot()
    {
        Instantiate(bullet, pos.transform.position, Quaternion.identity);
    }
}
