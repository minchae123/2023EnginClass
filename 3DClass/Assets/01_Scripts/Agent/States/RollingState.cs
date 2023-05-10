using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : CommonState
{
    [SerializeField] private float rollingSpeed = 0.4f, animationThreadhold = 0.1f;

    private float timer = 0;

    public override void OnEnterState()
    {
        animator.OnAnimationEndTrigger += RollingEndHandle;
        animator.SetRollingState(true);
        agentMovement.IsActiveMove = false; // ��� ���� ���� ȸ����ų�� ���� ������ ���콺 ����
        //Vector3 mousePos = agentInput.GetMouseWorldPosition();
        //Vector3 dir = mousePos - agentController.transform.position;
        //dir.y = 0;
        //agentMovement.SetMovementVelocity(- dir.normalized * rollingSpeed);

        Vector3 keyDir = agentInput.GetCurrentInputDirection();
        if(keyDir.magnitude < 0.1f)
        {
            keyDir = agentController.transform.forward;
        }
        agentMovement.SetRotation(keyDir + agentController.transform.position);
        agentMovement.SetMovementVelocity(keyDir.normalized * rollingSpeed);
        timer = 0;
    }

    public override void OnExitState()
    {
        animator.OnAnimationEndTrigger -= RollingEndHandle;
        agentMovement.IsActiveMove = true;
        animator.SetRollingState(false);
    }


    private void RollingEndHandle()
    {
        if (timer < animationThreadhold) return;
        agentMovement.StopImmediately();
        agentController.ChangeState(Core.StateType.Normal);
    }

    public override bool UpdateState()
    {
        timer += Time.deltaTime;
        return true;
    }

}