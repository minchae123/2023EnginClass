using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState aiState;

    [SerializeField] private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private NavMeshAgent navAgent;

    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void SetDestination(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }

    public void ChangeState(AIState nextState)
    {
        aiState = nextState;
    }

    private void Update()
    {
        aiState?.UpdateState();
    }
}
