using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentHealth : MonoBehaviour, IDamageable
{
    public UnityEvent<int, Vector3, Vector3> OnHitTriggered = null;
    public UnityEvent OnDeadTriggered = null;

    public Action<int, int> OnHealthChanged;

    [SerializeField] private HealthAndArmorSO healthAndArmor;
    private int curHealth;

    private AgentController agentController;

    private void Awake()
    {
        agentController = GetComponent<AgentController>();
    }

    private void Start()
    {
        curHealth = healthAndArmor.MaxHP;
    }

    public void AddHealth(int value)
    {
        curHealth = Mathf.Clamp(curHealth + value, 0, healthAndArmor.MaxHP);
    }

    public void OnDamage(int damage, Vector3 point, Vector3 normal)
    {
        if (agentController.IsDead) return;

        int calcDamage = Mathf.CeilToInt(damage - (damage * healthAndArmor.ArmorValue));
        AddHealth(-calcDamage);

        if(curHealth == 0)
        {
            OnDeadTriggered?.Invoke();
        }
        else
        {
            agentController.ChangeState(Core.StateType.OnHit);
        }

        OnHitTriggered?.Invoke(calcDamage, point, normal);
    }
}
