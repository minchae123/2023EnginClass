using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    [SerializeField]
    private float _knockBackSpeed = 20f, _gravity = -9.8f, _knockBackTime = 0.4f;
    private float _verticalVelocity;
    private Vector3 _knockBackVelocity;
    private Vector3 _movementVelocity;

    private NavMeshAgent _navAgent;
    public NavMeshAgent NavAgent => _navAgent;

    private CharacterController _characterController;
    private bool _isControllerMode = false;
    private float _knockBackStartTime;
    private Action EndKnockBackAction;

    private AIActionData _aiActionData;

    protected virtual void Awake()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        _characterController = GetComponent<CharacterController>();
        _aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetSpeed(float speed)
    {
        _navAgent.speed = speed; //�̵� �ӵ� SO�� ���缭 �����ϵ���
    }

    public void MoveToTarget(Vector3 pos)
    {
        _navAgent.SetDestination(pos);
    }

    public bool CheckIsArrived()
    {
        if(_navAgent.pathPending == false && _navAgent.remainingDistance <= _navAgent.stoppingDistance)
        {
            return true;
        }

        return false;
    }

    public void StopImmediately()
    {
        _navAgent.SetDestination(transform.position); //�ڱ��ڽ��� �������� ������ �ٷ� ������.
    }

    public void StopNavigation()
    {
        _navAgent.isStopped = true; //�׺� ������Ʈ�� �����ְ�
    }

    public void KnockBack(Action EndAction = null)
    {
        //_navAgent.updatePosition = false;
        //_navAgent.updateRotation = false;

        _navAgent.enabled = false;
        _knockBackStartTime = Time.time; //�˹� ���� �ð��� ����
        _isControllerMode = true;
        _knockBackVelocity = _aiActionData.HitNormal * -1 * _knockBackSpeed;

        EndKnockBackAction = EndAction;
    }

    private bool CalculateKnockBackMovement()
    {
        float spendTime = Time.time - _knockBackStartTime;
        float ratio = spendTime / _knockBackTime;
        _movementVelocity = Vector3.Lerp(_knockBackVelocity, Vector3.zero, ratio) 
                                * Time.fixedDeltaTime;
        return ratio < 1;
    }

    public void ResetNavAgent()
    {
        _characterController.enabled = true;
        _navAgent.enabled = true;
        _navAgent.isStopped = false;
    }

    private void FixedUpdate()
    {
        if (_isControllerMode == false) return;

        if(_characterController.isGrounded == false)
        {
            _verticalVelocity = _gravity * Time.fixedDeltaTime;
        }else
        {
            _verticalVelocity = _gravity * 0.3f * Time.fixedDeltaTime;
        }

        if(CalculateKnockBackMovement())
        {
            Vector3 move = _movementVelocity + _verticalVelocity * Vector3.up;
            _characterController.Move(move);
        }else
        {
            _isControllerMode = false; //��Ʈ�ѷ� ��� ���ְ�
            _characterController.enabled = false;
            EndKnockBackAction?.Invoke();
        }
    }
}
