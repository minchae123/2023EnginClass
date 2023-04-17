using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="SO/EnemyData")]
public class EnemyDataSO : ScriptableObject
{
    public int maxHP;
    public float moveSpeed;
    public float rotateSpeed;
    public int atkDamage;
    public float atkCoolTime;
}
