using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAction : AIAction
{
    protected MeshRenderer _renderer;
    protected override void Awake()
    {
        base.Awake();
        _renderer = brain.GetComponent<MeshRenderer>();
    }
    public override void TakeAction()
    {
        // DO NOTHING
        _renderer.material.color = new Color(0, 0, 1);
    }
}
