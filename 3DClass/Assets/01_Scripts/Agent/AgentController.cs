using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

public class AgentController : MonoBehaviour
{
    [SerializeField] private CharacterDataSO charData;
    public CharacterDataSO CharData => charData;

    private Dictionary<StateType, Istate> stateDictionary = null;
    private Istate currentState; // ���� ���� ����

    public String CurrentState;

    public AgentMovement AgentMovementCompo { get; private set; }
    public DamageCaster DamageCasterCompo { get; private set; }

    private void Awake()
    {
        stateDictionary = new Dictionary<StateType, Istate>();
        Transform stateTrm = transform.Find("States");

        foreach(StateType state in Enum.GetValues(typeof(StateType)))
        {
            Istate stateScript = stateTrm.GetComponent($"{state}State") as Istate;
            if(stateScript == null)
            {
                Debug.LogError($"There is no script : {state}");
                return;
            }
            stateScript.SetUp(transform);

            stateDictionary.Add(state, stateScript);
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
        currentState?.OnExitState(); //���� ���� ������
        currentState = stateDictionary[type];
        currentState?.OnEnterState(); //�������� ����
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
