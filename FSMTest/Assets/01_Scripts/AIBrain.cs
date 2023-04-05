using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIState aiState;

    public void ChangeState(AIState nextState)
    {
        aiState = nextState;
    }

    private void Update()
    {
        aiState?.UpdateState();
    }
}
