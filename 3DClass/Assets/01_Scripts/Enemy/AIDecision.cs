using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIDecision : MonoBehaviour
{
    public bool IsReverse = false;
    protected AIActionData aiActionData;
    protected EnemyController enemyController;

    public virtual void SetUp(Transform enemyRoot)
    {
        enemyController = enemyRoot.GetComponent<EnemyController>();
        aiActionData = enemyRoot.Find("AI").GetComponent<AIActionData>();
    }

    public abstract bool MakeADecision();
}
