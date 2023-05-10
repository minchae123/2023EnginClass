using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHitDecision : AIDecision
{
    public override bool MakeADecision()
    {
        return aiActionData.IsHit;
    }
}
