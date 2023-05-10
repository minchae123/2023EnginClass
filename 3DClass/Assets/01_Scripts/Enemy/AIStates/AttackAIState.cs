using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    [SerializeField] private float rotateSpeed = 720f;

    protected Vector3 targetVector;
    private bool IsAttack = false;
    private int atkDamage = 1;
    private float atkCoolTime = 0.2f;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        rotateSpeed = enemyController.EnemyData.rotateSpeed;
        atkDamage = enemyController.EnemyData.atkDamage;
        atkCoolTime = enemyController.EnemyData.atkCoolTime;
    }

    public override void OnEnterState()
    {
        enemyController.NavMovement.StopImmediately();
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackAnimationEndHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollsionHandle;
        aiActionData.IsAttacking = false;

        IsAttack = true;
    }

    public override void OnExitState()
    {
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollsionHandle;
        enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackAnimationEndHandle;

        enemyController.AgentAnimator.SetAttackState(false);
        enemyController.AgentAnimator.SetAttackTrigger(false);
        IsAttack = false;
    }

    private void AttackAnimationEndHandle()
    {
        enemyController.AgentAnimator.SetAttackState(false);
        MonoFunction.Instance.AddCoroutine(() => { aiActionData.IsAttacking = false; }, atkCoolTime);
    }

    private void AttackCollsionHandle()
    {
        
    }

    public override bool UpdateState()
    {
        if (base.UpdateState())
        {
            return true;
        }

        if(aiActionData.IsAttacking == false && IsAttack)
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
        return false;
    }

    private void SetTarget()
    {
        targetVector = enemyController.TargetTrm.position - transform.position;
        targetVector.y = 0;
    }
}
