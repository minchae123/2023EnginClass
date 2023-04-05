using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CommonAIState currentState;
    
    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    protected virtual void Awake()
    {
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        states.ForEach(s => s.SetUp(transform));
    }

    protected virtual void Start()
    {
        targetTrm = GameManager.Instance.PlayerTrm;
    }

    public void ChangeState(CommonAIState state)
    {
        currentState?.OnExitState();
        currentState = state;
        currentState?.OnEnterState();
    }

    protected virtual void Update()
    {
        currentState?.UpdateState();
    }
}
