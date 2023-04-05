using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIState : MonoBehaviour
{
    public List<AITransition> Transitions = null;
    public List<AIAction> Actions = null;

    protected AIBrain brain;
    private void Awake()
    {
        brain = transform.GetComponentInParent<AIBrain>();
        if (brain == null)
        {
            Debug.LogError("³ú ºñ¾úÀ½");
        }
        Actions = new List<AIAction>();
        GetComponents<AIAction>(Actions);
    }

    public void UpdateState()
    {
        foreach(AIAction a in Actions)
        {
            a.TakeAction();
        }

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
