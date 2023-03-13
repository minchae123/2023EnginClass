using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int speedHash = Animator.StringToHash("speed");
    private readonly int isAirboneHash = Animator.StringToHash("is_airbone");

    private Animator animator;
    public Animator Animator => Animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetSpeed(float value)
    {
        animator.SetFloat(speedHash, value);
    }

    public void SetAirbone(bool value)
    {
        animator.SetBool(isAirboneHash, value);
    }
}
