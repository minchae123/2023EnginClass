using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    [SerializeField] private float rotateSpeed = 720f;

    protected Vector3 targetVector;

    public override void OnEnterState()
    {
        enemyController.NavMovement.StopImmediately();
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackAnimationEndHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollsionHandle;
        aiActionData.IsAttacking = false;
    }

    public override void OnExitState()
    {
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollsionHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackAnimationEndHandle;

        enemyController.AgentAnimator.SetAttackState(false);
        enemyController.AgentAnimator.SetAttackTrigger(false);
    }

    private void AttackAnimationEndHandle()
    {
        enemyController.AgentAnimator.SetAttackState(false);
        aiActionData.IsAttacking = false;
    }

    private void AttackCollsionHandle()
    {
        
    }

    public override void UpdateState()
    {
        if(aiActionData.IsAttacking == false)
        {
            SetTarget();

            Vector3 curFrontVector = transform.forward; // 캐릭터 전방
            float angle = Vector3.Angle(curFrontVector, targetVector);

            if(angle >= 10)
            {
                Vector3 result = Vector3.Cross(curFrontVector, targetVector);

                float sign = result.y > 0 ? 1 : -1;
                enemyController.transform.rotation = Quaternion.Euler(0, sign * rotateSpeed * Time.deltaTime, 0) * enemyController.transform.rotation;
            }
            else
            {
                aiActionData.IsAttacking = true;
                enemyController.AgentAnimator.SetAttackState(true);
                enemyController.AgentAnimator.SetAttackTrigger(true);
            }
        }

        base.UpdateState();
    }

    private void SetTarget()
    {
        targetVector = enemyController.TargetTrm.position - transform.position;
        targetVector.y = 0;
    }
}
