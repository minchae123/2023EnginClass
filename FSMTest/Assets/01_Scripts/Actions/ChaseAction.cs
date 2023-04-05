using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAction : AIAction
{
    protected MeshRenderer _renderer;

    protected override void Awake()
    {
        base.Awake();
        _renderer = brain.GetComponent<MeshRenderer>();
    }
    public override void TakeAction()
    {
        _renderer.material.color = new Color(1, 0, 0);
        brain.SetDestination(brain.TargetTrm.position);
    }
}
