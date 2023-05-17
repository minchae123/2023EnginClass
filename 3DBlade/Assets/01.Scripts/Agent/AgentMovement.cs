using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentMovement : MonoBehaviour
{
    public bool IsActiveMove = true; //Ű����� �̵��ϳ�? �ƴϳ�

    [SerializeField]
    private float _gravity = -9.8f;

    private CharacterController _characterController;

    private Vector3 _movementVelocity;
    public Vector3 MovementVelocity => _movementVelocity; //���ӵ�
    private float _verticalVelocity; //�߷¼ӵ�

    private AgentAnimator _animator;

    private Vector3 _inputVelocity;
    private AgentController _controller;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = transform.Find("Visual").GetComponent<AgentAnimator>();
    }

    public void SetInit(AgentController controller)
    {
        _controller = controller;
    }

    public void SetMovementVelocity(Vector3 value)
    {
        _inputVelocity = value;
        _movementVelocity = value;
    }

    private void CalculatePlayerMovement()
    {
        //���Ⱑ �ٽ�
        _animator?.SetSpeed(_inputVelocity.sqrMagnitude); //�߰���.

        _inputVelocity.Normalize();

        float moveSpeed = _controller.CharData.MoveSpeed;
        _movementVelocity = _inputVelocity * (moveSpeed * Time.fixedDeltaTime);
        _movementVelocity = Quaternion.Euler(0, -45f, 0) * _movementVelocity;

        if(_movementVelocity.sqrMagnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(_movementVelocity);
            //������ ���� ���� �ϱ�
        }
    }

    public void StopImmediately()
    {
        _movementVelocity = Vector3.zero;
        _animator?.SetSpeed(_movementVelocity.sqrMagnitude); //�߰���.
    }

    public void SetRotation(Vector3 targetPos)  //������ �ٶ󺸴� �ڵ�
    {
        Vector3 dir = targetPos - transform.position;
        dir.y = 0;
        transform.rotation = Quaternion.LookRotation(dir);
    }

    private void FixedUpdate()
    {
        if(IsActiveMove)
            CalculatePlayerMovement(); //�÷��̾� �̼� ��� (Ű���� �Է½ÿ��� 45�����)

        if(_characterController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }
        else
        {
            //0.3�� �ϵ��ڵ��� ��
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }

        Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
        _characterController.Move(move);

        _animator?.SetAirbone(_characterController.isGrounded == false);
    }
}
