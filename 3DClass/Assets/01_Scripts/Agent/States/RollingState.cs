using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingState : CommonState
{
    [SerializeField] private float rollingSpeed = 0.4f;

    public override void OnEnterState()
    {
        animator.OnAnimationEndTrigger += RollingEndHandle;
        animator.SetRollingState(true);
        agentMovement.IsActiveMove = false; // 어느 방향 보고 회전시킬지 결정 지금은 마우스 방향
        //Vector3 mousePos = agentInput.GetMouseWorldPosition();
        //Vector3 dir = mousePos - agentController.transform.position;
        //dir.y = 0;
        Vector3 dir = agentMovement.MovementVelocity - agentController.transform.position;
        dir = Quaternion.Euler(0, 90f, 0) * dir;
        agentMovement.SetMovementVelocity(- dir.normalized * rollingSpeed);
    }

    public override void OnExitState()
    {
        animator.OnAnimationEndTrigger -= RollingEndHandle;
        agentMovement.IsActiveMove = true;
        animator.SetRollingState(false);
    }


    private void RollingEndHandle()
    {
        agentMovement.StopImmediately();
        agentController.ChangeState(Core.StateType.Normal);
    }

    public override void UpdateState()
    {
        
    }

}