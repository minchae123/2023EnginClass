using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAnimator : MonoBehaviour
{
    private readonly int hashMoveX = Animator.StringToHash("move_x");
    private readonly int hashMoveY = Animator.StringToHash("move_y");
    private readonly int hashIsMoving = Animator.StringToHash("is_moving");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SetMoving(bool value)
    {
        animator.SetBool(hashIsMoving, value);
    }

    public void SetDirection(Vector2 dir)
    {
        animator.SetFloat(hashMoveX, dir.x);
        animator.SetFloat(hashMoveY, dir.y);
    }
}
