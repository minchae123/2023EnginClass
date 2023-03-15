using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    [SerializeField] private float keyDelay = 0.5f;

    private int currentCombo = 1; // 현재 콤보
    private bool canAttack; // 선입력 막기
    private float keyTimer = 0; // 다음 공격이 이뤄지기 까지 기다리는 시간 이 시간내로 입력 안하면 idle로 돌아감

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        currentCombo = 0;
        canAttack = true;
        animator.SetAttackState(true);
        OnAttackHandle(); // 처음 1타 들어가도록
    }

    public override void OnExitState()
    {
        agentInput.OnAttackKeyPress -= OnAttackHandle;
        animator.OnAnimationEndTrigger -= OnAnimationEnd;
        animator.SetAttackState(false);
        animator.SetAttackTrigger(false);
    }

    private void OnAnimationEnd()
    {
        canAttack = true;
        keyTimer = keyDelay; 
    }

    public void OnAttackHandle()
    {
        if(canAttack && currentCombo < 3)
        {
            canAttack = false;
            currentCombo++;
            animator.SetAttackTrigger(true);
        }
    }

    public override void UpdateState()
    {
        if(canAttack && keyTimer > 0)
        {
            keyTimer -= Time.deltaTime;
            if(keyTimer <= 0)
            {
                agentController.ChangeState(StateType.Normal);
            }
        }
    }
}
