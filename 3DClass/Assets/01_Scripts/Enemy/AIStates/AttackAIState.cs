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
        base.UpdateState();

        if(aiActionData.IsAttacking == false)
        {
            SetTarget();
            Quaternion rot = Quaternion.LookRotation(targetVector);
            transform.rotation = rot;

            aiActionData.IsAttacking = true;
            enemyController.AgentAnimator.SetAttackState(true);
            enemyController.AgentAnimator.SetAttackTrigger(true);
        }
    }

    private void SetTarget()
    {
        targetVector = enemyController.TargetTrm.position - transform.position;
        targetVector.y = 0;
    }
}
