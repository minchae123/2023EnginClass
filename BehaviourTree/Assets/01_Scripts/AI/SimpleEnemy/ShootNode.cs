using BehaviourTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShootNode : Node
{
    private NavMeshAgent agent;
    private EnemyBrain brain;
    private float coolTime = 0;
    private float lastFireTime = 0;
    
    public ShootNode(NavMeshAgent agent, EnemyBrain brain, float coolTime )
    {
        this.agent = agent;
        this.brain = brain;
        this.coolTime = coolTime;
    }

    public override NodeState Evaluate()
    {
        agent.isStopped = true;

        if(lastFireTime + coolTime <= Time.time)
        {
            brain.TryToTalk("Attack", 1f);
        }

        return NodeState.RUNNING;
    }
}
