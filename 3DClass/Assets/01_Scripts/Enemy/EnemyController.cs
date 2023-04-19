using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemyDataSO enemyData;
    public EnemyDataSO EnemyData => enemyData;
    [SerializeField] private CommonAIState currentState;
    
    private Transform targetTrm;
    public Transform TargetTrm => targetTrm;

    private NavAgentMovement navMovement;
    public NavAgentMovement NavMovement => navMovement;

    private AgentAnimator agentAnimator;
    public AgentAnimator AgentAnimator => agentAnimator;

    private EnemyVFXManager vFXManager;
    public EnemyVFXManager VFXManager => vFXManager;

    public EnemyHealth EnemyHealthCompo { get; private set; }

    public bool isDead = false;

    protected virtual void Awake()
    {
        navMovement = GetComponent<NavAgentMovement>();
        agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
        vFXManager = GetComponent<EnemyVFXManager>();

        EnemyHealthCompo = GetComponent<EnemyHealth>();
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        states.ForEach(s => s.SetUp(transform));
    }

    protected virtual void Start()
    {
        targetTrm = GameManager.Instance.PlayerTrm;
        navMovement.SetSpeed(enemyData.moveSpeed); // �̼� ����
        EnemyHealthCompo.SetMaxHP(enemyData.maxHP); // ü�� ����
    }

    public void ChangeState(CommonAIState state)
    {
        currentState?.OnExitState();
        currentState = state;
        currentState?.OnEnterState();
    }

    protected virtual void Update()
    {
        if (isDead) return;
        currentState?.UpdateState();
    }

    public UnityEvent OnAfterDeathTrigger = null;
    public void SetDead()
    {
        isDead = true;
        navMovement.StopNavigation();
        agentAnimator.StopAnimation(true);
        navMovement.KnockBack(() =>
        {
            agentAnimator.StopAnimation(false);
            agentAnimator.SetDead();

            MonoFunction.Instance.AddCoroutine(() =>
            {
                OnAfterDeathTrigger?.Invoke();
            }, 1.5f);
        });
    }
}
