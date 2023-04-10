using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int speedHash = Animator.StringToHash("speed");
    private readonly int isAirboneHash = Animator.StringToHash("is_airbone");
    private readonly int attackHash = Animator.StringToHash("attack");
    private readonly int isAttackHash = Animator.StringToHash("is_attack");
    private readonly int isRollingHash = Animator.StringToHash("is_rolling");

    public event Action OnAnimationEndTrigger = null;
    public event Action OnAnimationEventTrigger = null;

    private Animator animator;
    public Animator Animator => animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void OnAnimationEvent()
    {
        OnAnimationEventTrigger?.Invoke();
    }

    public void SetRollingState(bool value)
    {
        animator.SetBool(isRollingHash, value);
    }

    public void SetSpeed(float value)
    {
        animator.SetFloat(speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        animator.SetBool(isAirboneHash, value);
    }

    public void SetAttackState(bool value)
    {
        animator.SetBool(isAttackHash, value);
    }

    public void SetAttackTrigger(bool value)
    {
        if (value)
        {
            animator.SetTrigger(attackHash);
        }
        else
        {
            animator.ResetTrigger(attackHash);
        }
    }

    public void OnAnimationEnd()
    {
        OnAnimationEndTrigger?.Invoke();
    }
}
