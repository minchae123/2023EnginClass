using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public List<AIDecision> Decisions;
    public AIState NextState;

    protected AIBrain brain;
    private void Awake()
    {
        brain = transform.GetComponentInParent<AIBrain>();
        if(brain == null)
        {
            Debug.LogError("³ú ºñ¾úÀ½");
        }
    }

    public bool CheckTransition()
    {
        bool result = false;

        foreach(AIDecision d in Decisions)
        {
            result = d.MakeDecision();
            
            if (d.IsReverse)
                result = !result;
            
            if(result == false)
            {
                break;
            }
        }

        return result;
    }
}