using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviourTree;

public class RangeNode : Node
{
    private float range;
    private Transform target;
    private Transform bodyTrm;

    public RangeNode(float range, Transform target, Transform body)
    {
        this.range = range;
        this.target = target;
        bodyTrm = body;
    }

    public override NodeState Evaluate()
    {
        float distance = Vector3.Distance(bodyTrm.position, target.position);

        return distance < range ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}