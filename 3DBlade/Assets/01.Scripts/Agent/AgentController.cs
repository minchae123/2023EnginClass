using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentController : MonoBehaviour
{
    [SerializeField]
    private CharacterDataSO _charDataSO;
    public CharacterDataSO CharData => _charDataSO;

    private Dictionary<StateType, IState> _stateDictionary = null;  //������ �ִ� ���µ� ����
    private IState _currentState; //���� ���� ����

    public String CurrentState;

    public AgentMovement AgentMovementCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }

    public bool IsDead { get; private set; }
    public AgentHealth AgentHelathCompo { get; private set; }

    public void SetDead()
    {
        IsDead = true;
    }


    private void Awake()
    {
        _stateDictionary = new Dictionary<StateType, IState>();
        Transform stateTrm = transform.Find("States");

        AgentHelathCompo = GetComponent<AgentHealth>();

        foreach( StateType state in Enum.GetValues(typeof (StateType)) )
        {
            IState stateScript = stateTrm.GetComponent($"{state}State") as IState;
            if(stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(transform);
            _stateDictionary.Add(state, stateScript);
        }

        AgentMovementCompo = GetComponent<AgentMovement>();
        AgentMovementCompo.SetInit(this);

        DamageCasterCompo = transform.Find("DamageCaster").GetComponent<DamageCaster>();
        DamageCasterCompo.SetInit(this);
    }


    private void Start()
    {
        ChangeState(StateType.Normal);
    }

    public void ChangeState(StateType type)
    {
        _currentState?.OnExitState(); //���� ���� ������
        _currentState = _stateDictionary[type];
        _currentState?.OnEnterState(); //�������� ����
    }

    private void Update()
    {
        if (IsDead) return;
        _currentState?.UpdateState();
    }
}
