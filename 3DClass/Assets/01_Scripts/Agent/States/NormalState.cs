using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;

public class NormalState : CommonState
{
    public override void OnEnterState()
    {
        agentMovement?.StopImmediately();
        agentInput.OnMovementKeyPress += OnMovementHandle;
        agentInput.OnAttackKeyPress += OnAttackKeyHandle;
        agentInput.OnRollingKeyPress += OnRollingHandle;
    }

    public override void OnExitState()
    {
        agentMovement?.StopImmediately();
        agentInput.OnMovementKeyPress -= OnMovementHandle;
        agentInput.OnAttackKeyPress -= OnAttackKeyHandle;
        agentInput.OnRollingKeyPress -= OnRollingHandle;
    }

    private void OnRollingHandle()
    {
        agentController.ChangeState(StateType.Rolling);
    }

    private void OnAttackKeyHandle()
    {
        Vector3 targetPos = agentInput.GetMouseWorldPosition();
        //agentMovement.SetRotation(targetPos);
        agentController.ChangeState(StateType.Attack);
    }

    private void OnMovementHandle(Vector3 obj)
    {
        agentMovement.SetMovementVelocity(obj);
    }

    /*public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
    }*/


    public override void UpdateState()
    {

    }
}
