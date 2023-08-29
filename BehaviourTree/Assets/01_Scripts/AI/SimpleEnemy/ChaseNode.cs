using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseNode : Node
{
    private Transform target;
    private NavMeshAgent agent;
    private EnemyBrain brain;

    public ChaseNode(Transform target, NavMeshAgent agent, EnemyBrain brain)
    {
        this.target = target;
        this.agent = agent;
        this.brain = brain;
        nodeState = NodeState.SUCCESS;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(target.position, agent.transform.position);
        if(distance >= 0.2f)
        {
            agent.isStopped = false;
            agent.SetDestination(target.position);

            if(nodeState!= NodeState.RUNNING)
            {
                brain.TryToTalk("Chasing!", 1f);
                nodeState = NodeState.RUNNING;
            }
        }
        else
        {
            agent.isStopped = true;
            nodeState = NodeState.SUCCESS;
        }
        return nodeState;
    }
}
