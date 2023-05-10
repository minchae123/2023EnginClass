using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackState : CommonState
{
    public event Action<int> OnAttackStart = null;
    public event Action OnAttackEnd = null;

    [SerializeField] private float keyDelay = 0.5f;

    private int currentCombo = 1; // ���� �޺�
    private bool canAttack; // ���Է� ����
    private float keyTimer = 0; // ���� ������ �̷����� ���� ��ٸ��� �ð� �� �ð����� �Է� ���ϸ� idle�� ���ư�

    private float attackStartTime; // ������ ���۵� �ð� ���
    [SerializeField] private float attackSlideDuration = 0.2f, attackSlideSpeed = 0.1f; // �����̵� �Ǵ� �ð�, ���ǵ�
    private DamageCaster damageCaster;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        damageCaster = agentRoot.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        agentInput.OnRollingKeyPress += OnRollingHandle;
        animator.OnAnimationEventTrigger += OnDamageCastHadle;
        currentCombo = 0;
        canAttack = true;
        animator.SetAttackState(true);

        agentMovement.IsActiveMove = false;
        Vector3 pos = agentInput.GetMouseWorldPosition();
        agentMovement.SetRotation(pos);
        OnAttackHandle(); // ó�� 1Ÿ ������
    }

    private void OnDamageCastHadle()
    {
        damageCaster.CastDamage();
    }

    public override void OnExitState()
    {
        agentInput.OnAttackKeyPress -= OnAttackHandle;
        animator.OnAnimationEndTrigger -= OnAnimationEnd;
        agentInput.OnRollingKeyPress -= OnRollingHandle;
        animator.OnAnimationEventTrigger -= OnDamageCastHadle;
        animator.SetAttackState(false);
        animator.SetAttackTrigger(false);

        agentMovement.IsActiveMove = true; // Ű���� �̵� Ǯ��

        OnAttackEnd?.Invoke();
    }

    private void OnRollingHandle()
    {
        agentController.ChangeState(StateType.Rolling);
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
            attackStartTime = Time.time;
            canAttack = false;
            currentCombo++;
            animator.SetAttackTrigger(true);
            OnAttackStart?.Invoke(currentCombo);
        }
    }

    public override bool UpdateState()
    {
        if(Time.time < attackStartTime + attackSlideDuration) // �����̵尡 �ǰ� �־���ϴ� �ð�
        {
            float timePassed = Time.time - attackStartTime; // ���� �귯�� �ð�
            float lerpTime = timePassed / attackSlideDuration; // 0~1 ����

            Vector3 targetSpeed = Vector3.Lerp(agentController.transform.forward * attackSlideSpeed, Vector3.zero, lerpTime);
            agentMovement.SetMovementVelocity(targetSpeed);
        }

        if(canAttack && keyTimer > 0)
        {
            keyTimer -= Time.deltaTime;
            if(keyTimer <= 0)
            {
                agentController.ChangeState(StateType.Normal);
            }
        }
        return true;
    }
}