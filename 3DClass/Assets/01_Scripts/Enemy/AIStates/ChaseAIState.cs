using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAIState : CommonAIState
{
    public override void OnEnterState()
    {
        enemyController.AgentAnimator.SetSpeed(0.2f);
        enemyController.AgentAnimator.OnAnimationEventTrigger += FootStepHandle;
    }

    public override void OnExitState()
    {
        enemyController.AgentAnimator.SetSpeed(0);
        enemyController.AgentAnimator.OnAnimationEventTrigger -= FootStepHandle;
    }

    private void FootStepHandle()
    {
        enemyController.VFXManager.PlayFootStep();
    }

    public override void UpdateState()
    {
        enemyController.NavMovement.MoveToTarget(aiActionData.LastSpot);
        aiActionData.IsArrived = enemyController.NavMovement.CheckisArrived();
        base.UpdateState();
    }
}
