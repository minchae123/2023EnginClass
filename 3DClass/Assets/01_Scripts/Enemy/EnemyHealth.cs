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

    private EnemyController _controller;

    public int maxHP;
    private int curHP;

    public void SetMaxHP(int value)
    {
        curHP = maxHP = value;
    }

    private void Awake()
    {
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void OnDamage(int damage, Vector3 point, Vector3 nomal)
    {
        if ( _controller.isDead ) return;

        aiActionData.HitPoint = point;
        aiActionData.HitNormal = nomal;
        aiActionData.IsHit = true;

        OnHitTriggered?.Invoke();

        curHP -= damage;
        curHP = Mathf.Clamp(curHP, 0, maxHP);

        if(curHP <= 0)
        {
            OnDeadTriggered?.Invoke();
        }

        UIManager.Instance.Subscribe(this);

        OnHealthChanged?.Invoke(curHP, maxHP);
    }

    public void SetInit(EnemyController controller)
    {
        _controller = controller; 
    }
}
