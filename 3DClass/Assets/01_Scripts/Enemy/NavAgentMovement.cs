using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavAgentMovement : MonoBehaviour
{
    private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent => navAgent;

    protected virtual void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    public void MoveToTarget(Vector3 pos)
    {
        navAgent.SetDestination(pos);
    }

    public bool CheckisArrived()
    {
        if(navAgent.pathPending == false && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            return true;
        }
        return false;
    }

    public void StopImmediately()
    {
        navAgent.SetDestination(transform.position); // 자기 자신을 목적지로 놓으면 바로 정지됨
    }
}
