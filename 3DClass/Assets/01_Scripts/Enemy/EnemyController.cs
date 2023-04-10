using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private CommonAIState currentState;
    
    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private NavAgentMovement navMovement;
    public NavAgentMovement NavMovement => navMovement;

    private AgentAnimator agentAnimator;
    public AgentAnimator AgentAnimator => agentAnimator;

    private EnemyVFXManager vFXManager;
    public EnemyVFXManager VFXManager => vFXManager;

    protected virtual void Awake()
    {
        navMovement = GetComponent<NavAgentMovement>();
        agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
        vFXManager = GetComponent<EnemyVFXManager>();

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
