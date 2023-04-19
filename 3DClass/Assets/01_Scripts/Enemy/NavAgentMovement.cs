using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    [SerializeField] private float knockBackSpeed = 20f, grivity = -9.8f;
    [SerializeField]private float knockBackTime = 0.4f;
    private float verticalVelocity;
    private Vector3 knockBackVelocity;
    private Vector3 movementVelocity;

    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent => navAgent;

    private CharacterController characterController;
    private bool isControllerMode = false;
    private float knockBackStartTime;
    private Action EndKnockBackAction;

    private AIActionData aiActionData;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        characterController = GetComponent<CharacterController>();
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void SetSpeed(float speed)
    {
        navAgent.speed = speed; // 이동 속도에 맞춰 실행
    }

    public void MoveToTarget(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }

    public bool CheckisArrived()
    {
        if(navAgent.pathPending == false && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    public void StopImmediately()
    {
        navAgent.SetDestination(transform.position); // 자기 자신을 목적지로 놓으면 바로 정지됨
    }

    public void StopNavigation()
    {
        navAgent.isStopped = true; // 네브 에이전트 멈춤
    }

    public void KnockBack(Action endAction = null)
    {
        //navAgent.updatePosition = false;
        //navAgent.updateRotation = false;

        navAgent.enabled = false;
        knockBackStartTime = Time.time;
        isControllerMode = true;
        knockBackVelocity = aiActionData.HitNormal * -1 * knockBackSpeed;

        EndKnockBackAction = endAction;
    }

    private bool CalculateKnockBackMovement()
    {
        float spendTime = Time.time - knockBackStartTime;
        float ratio = spendTime / knockBackTime;
        movementVelocity = Vector3.Lerp(knockBackVelocity, Vector3.zero, ratio) * Time.fixedDeltaTime;

        return ratio < 1;
    }

    private void FixedUpdate()
    {
        if (isControllerMode == false) return;

        if(characterController.isGrounded == false)
        {
            verticalVelocity = grivity * Time.fixedDeltaTime;
        }
        else
        {
            verticalVelocity = grivity * 0.3f * Time.fixedDeltaTime;
        }

        if (CalculateKnockBackMovement())
        {
            Vector3 move = movementVelocity + verticalVelocity * Vector3.up;
            characterController.Move(move);
        }
        else
        {
            isControllerMode = false;
            characterController.enabled = false;
            EndKnockBackAction?.Invoke();
        }
    }
}
