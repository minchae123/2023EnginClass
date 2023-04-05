using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIAction : MonoBehaviour
{
    protected AIBrain brain;
    
    protected virtual void Awake()
    {
        brain = GetComponentInParent<AIBrain>();
    }

    public abstract void TakeAction();
}