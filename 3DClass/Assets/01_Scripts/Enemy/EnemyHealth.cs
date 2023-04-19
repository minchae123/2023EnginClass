using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnDeadTriggered = null; // »ç¸Á Æ®¸®°Å
    public UnityEvent OnHitTriggered = null;
    private AIActionData aiActionData;

    public Action<int, int> OnHealthChanged;

    [SerializeField] private bool isDead = false;

    public int maxHP;
    private int curHP;

    public void SetMaxHP(int value)
    {
        curHP = maxHP = value;
        isDead = false;
    }

    private void Awake()
    {
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void OnDamage(int damage, Vector3 point, Vector3 nomal)
    {
        aiActionData.HitPoint = point;
        aiActionData.HitNormal = nomal;
        OnHitTriggered?.Invoke();

        curHP -= damage;
        if(curHP <= 0)
        {
            isDead = true;
            OnDeadTriggered?.Invoke();
        }

        OnHealthChanged?.Invoke(curHP, maxHP);
    }
}
