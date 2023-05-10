using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitAIState : CommonAIState
{
    public override void OnEnterState()
    {
        enemyController.transform.rotation = Quaternion.LookRotation(aiActionData.HitNormal);

        enemyController.AgentAnimator.OnAnimationEndTrigger += AnimationEndHandle;
        enemyController.AgentAnimator.SetHurtTrigger(true);
    }

    public override void OnExitState()
    {
        enemyController.AgentAnimator.OnAnimationEndTrigger -= AnimationEndHandle;
        enemyController.AgentAnimator.SetHurtTrigger(false);
    }

    private void AnimationEndHandle()
    {
        aiActionData.IsHit = false;
    }
}
