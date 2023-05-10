using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, Istate
{
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract bool UpdateState();

    protected AgentAnimator animator;
    protected AgentInput agentInput;
    protected AgentController agentController;
    protected AgentMovement agentMovement;

    public virtual void SetUp(Transform agentRoot)
    {
        animator = agentRoot.Find("Visual").GetComponent<AgentAnimator>();
        agentInput = agentRoot.GetComponent<AgentInput>();
        agentController = agentRoot.GetComponent<AgentController>();
        agentMovement = agentRoot.GetComponent<AgentMovement>();
    }

    public void OnHitHandle(Vector3 hitPoint, Vector3 Normal)
    {

    }


}
