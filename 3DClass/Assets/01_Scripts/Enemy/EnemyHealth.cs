using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    public UnityEvent OnHitTriggered = null;
    private AIActionData aiActionData;

    private void Awake()
    {
        aiActionData = transform.Find("AI").GetComponent<AIActionData>();
    }

    public void OnDamage(int damage, Vector3 point, Vector3 nomal)
    {
        aiActionData.HitPoint = point;
        aiActionData.HitNormal = nomal;
        OnHitTriggered?.Invoke();
    }
}
