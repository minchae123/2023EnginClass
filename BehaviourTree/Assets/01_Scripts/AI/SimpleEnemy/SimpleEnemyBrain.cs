using BehaviourTree;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyBrain : EnemyBrain
{
    private Node topNode;
    private NodeState beforeTopState;

    protected void Start()
    {
        ConstructAIBrin();
    }

    private void ConstructAIBrin()
    {
        Transform me = transform;
        RangeNode shootingRange = new RangeNode(3f, targetTrm, me);
        ShootNode shootNode = new ShootNode(NavAgent, this, 1.5f);
        Sequence shootSeq = new Sequence(new List<Node> { shootingRange, shootNode });

        RangeNode chasingRange = new RangeNode(10f, targetTrm, me);
        ChaseNode chaseNode = new ChaseNode(targetTrm, NavAgent, this);
        Sequence chaseSeq = new Sequence(new List<Node> { chasingRange, chaseNode });

        topNode = new Selector(new List<Node> { shootSeq, chaseSeq });
    }

    private void Update()
    {
        topNode.Evaluate();
        if(topNode.NodeState == NodeState.FAILURE && beforeTopState != NodeState.FAILURE)
        {
            TryToTalk("아무것도 할게없어");
        }

        beforeTopState = topNode.NodeState;
    }
}