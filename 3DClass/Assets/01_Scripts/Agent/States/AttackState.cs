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

    private int currentCombo = 1; // 현재 콤보
    private bool canAttack; // 선입력 막기
    private float keyTimer = 0; // 다음 공격이 이뤄지기 까지 기다리는 시간 이 시간내로 입력 안하면 idle로 돌아감

    private float attackStartTime; // 공격이 시작된 시간 기록
    [SerializeField] private float attackSlideDuration = 0.2f, attackSlideSpeed = 0.1f; // 슬라이드 되는 시간, 스피드
    

    public override void OnEnterState()
    {
        agentInput.OnAttackKeyPress += OnAttackHandle;
        animator.OnAnimationEndTrigger += OnAnimationEnd;
        agentInput.OnRollingKeyPress += OnRollingHandle;
        currentCombo = 0;
        canAttack = true;
        animator.SetAttackState(true);

        agentMovement.IsActiveMove = false;
        Vector3 pos = agentInput.GetMouseWorldPosition();
        agentMovement.SetRotation(pos);
        OnAttackHandle(); // 처음 1타 들어가도록
    }

    public override void OnExitState()
    {
        agentInput.OnAttackKeyPress -= OnAttackHandle;
        animator.OnAnimationEndTrigger -= OnAnimationEnd;
        agentInput.OnRollingKeyPress -= OnRollingHandle;
        animator.SetAttackState(false);
        animator.SetAttackTrigger(false);

        agentMovement.IsActiveMove = true; // 키보드 이동 풀고

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

    public override void UpdateState()
    {
        if(Time.time < attackStartTime + attackSlideDuration) // 슬라이드가 되고 있어야하는 시간
        {
            float timePassed = Time.time - attackStartTime; // 현재 흘러간 시간
            float lerpTime = timePassed / attackSlideDuration; // 0~1 맵핑

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
    }
}