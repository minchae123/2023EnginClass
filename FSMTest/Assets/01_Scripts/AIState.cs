using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AITransition> Transitions = null;

    protected AIBrain brain;
    private void Awake()
    {
        brain = transform.GetComponentInParent<AIBrain>();
        if (brain == null)
        {
            Debug.LogError("³ú ºñ¾úÀ½");
        }
    }

    public void UpdateState()
    {
        foreach(AITransition t in Transitions)
        {
            if (t.CheckTransition())
            {
                brain.ChangeState(t.NextState);
                break;
            }
        }
    }
}
