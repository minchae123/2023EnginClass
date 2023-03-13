using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : CommonState
{
    protected AgentMovement agentMovement;

    public override void OnEnterState()
    {
        agentMovement?.StopImmediately();
        agentInput.OnMovementKeyPress += OnMovementHandle;
    }

    public override void OnExitState()
    {
        agentMovement?.StopImmediately();
        agentInput.OnMovementKeyPress -= OnMovementHandle;
    }

    private void OnMovementHandle(Vector3 obj)
    {
        agentMovement.SetMovementVelocity(obj);
    }

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        agentMovement = agentRoot.GetComponent<AgentMovement>();
    }

    public override void UpdateState()
    {

    }
}
