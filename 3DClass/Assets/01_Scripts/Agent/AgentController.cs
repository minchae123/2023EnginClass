using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using System;

public class AgentController : MonoBehaviour
{
    private Dictionary<StateType, Istate> stateDictionary = null;
    private Istate currentState; // 현재 상태 저장

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
    }

    private void Start()
    {
        ChangeState(StateType.Normal);
    }

    public void ChangeState(StateType type)
    {
        currentState?.OnExitState(); //현재 상태 나가고
        currentState = stateDictionary[type];
        currentState?.OnEnterState(); //다음상태 시작
    }

    private void Update()
    {
        currentState.UpdateState();
    }
}
