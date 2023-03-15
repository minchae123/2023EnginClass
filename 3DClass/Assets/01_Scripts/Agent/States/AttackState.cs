using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    [SerializeField] private float keyDelay = 0.5f;

    private int currentCombo = 1; // ���� �޺�
    private bool canAttack; // ���Է� ����
    private float keyTimer = 0; // ���� ������ �̷����� ���� ��ٸ��� �ð� �� �ð����� �Է� ���ϸ� idle�� ���ư�

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        currentCombo = 0;
        canAttack = true;
        animator.SetAttackState(true);
        OnAttackHandle(); // ó�� 1Ÿ ������
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
