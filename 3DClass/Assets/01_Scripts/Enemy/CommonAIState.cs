using System.Collections.Generic;
using UnityEngine;

public abstract class CommonAIState : MonoBehaviour, Istate
{
    protected List<AITransition> transitions;
    protected EnemyController enemyController;
    protected AIActionData aiActionData;

    public abstract void OnEnterState();
    public abstract void OnExitState();

    public void SetUp(Transform agentRoot)
    {
        enemyController = agentRoot.GetComponent<EnemyController>();
        aiActionData = agentRoot.Find("AI").GetComponent<AIActionData>();

        transitions = new List<AITransition>();
        transform.GetComponentsInChildren<AITransition>(transitions);

        transitions.ForEach(t => t.SetUp(agentRoot));
    }

    public virtual void UpdateState()
    {
        foreach(AITransition t  in transitions)
        {
            if (t.CheckTransition())
            {
                enemyController.ChangeState(t.NextState);
                break;
            }
        }
    }
}