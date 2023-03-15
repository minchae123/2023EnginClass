using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    private int currentCombo = 1; // ���� �޺�
    private bool canAttack; // ���Է� ����
    private float keyTimer = 0; // ���� ������ �̷����� ���� ��ٸ��� �ð� �� �ð����� �Է� ���ϸ� idle�� ���ư�

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        currentCombo = 0;
        canAttack = true;
        animator.SetAttackState(true);
        OnAttackHandle(); // ó�� 1Ÿ ������
    }

    public override void OnExitState()
    {
        agentInput.OnAttackKeyPress -= OnAttackHandle;
        animator.SetAttackState(false);
        animator.SetAttackTrigger(false);
    }
    
    public void OnAttackHandle()
    {
        if(canAttack && currentCombo < 3)
        {
           // canAttack = false;
            currentCombo++;
            animator.SetAttackTrigger(true);
        }
    }

    public override void UpdateState()
    {

    }
}
