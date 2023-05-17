using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonState : MonoBehaviour, IState
{
    public abstract void OnEnterState();
    public abstract void OnExitState();
    public abstract bool UpdateState();

    protected AgentAnimator _animator;
    protected AgentInput _agentInput;
    protected AgentController _agentController;
    protected AgentMovement _agentMovement; //�̵�����

    public virtual void SetUp(Transform agentRoot)
    {
        _animator = agentRoot.Find("Visual").GetComponent<AgentAnimator>();
        _agentInput = agentRoot.GetComponent<AgentInput>();
        _agentController = agentRoot.GetComponent<AgentController>();
        _agentMovement = agentRoot.GetComponent<AgentMovement>();
    }

    //�ǰ�ó���� ����� ��
    public void OnHitHandle(Vector3 hitPoint, Vector3 Normal)
    {

    }
}
