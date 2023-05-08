using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyController : PoolableMono
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

    private CommonAIState initState;

    protected virtual void Awake()
    {
        navMovement = GetComponent<NavAgentMovement>();
        agentAnimator = transform.Find("Visual").GetComponent<AgentAnimator>();
        vFXManager = GetComponent<EnemyVFXManager>();

        EnemyHealthCompo = GetComponent<EnemyHealth>();
        EnemyHealthCompo.SetInit(this);
        List<CommonAIState> states = new List<CommonAIState>();
        transform.Find("AI").GetComponentsInChildren<CommonAIState>(states);

        states.ForEach(s => s.SetUp(transform));

        initState = currentState;
    }

    protected virtual void Start()
    {
        targetTrm = GameManager.Instance.PlayerTrm;
        navMovement.SetSpeed(enemyData.moveSpeed); // 이속 설정
        EnemyHealthCompo.SetMaxHP(enemyData.maxHP); // 체력 설정
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

    public override void Init()
    {
        isDead = false;
        EnemyHealthCompo.SetMaxHP(enemyData.maxHP);
        navMovement.ResetNavAgent();
        ChangeState(initState);
    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }
}
