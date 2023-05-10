using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckStateDecision : AIDecision
{
    [SerializeField] private CommonAIState baseState;

    public override bool MakeADecision()
    {
        return enemyController.CurrentState == baseState;
    }
}
