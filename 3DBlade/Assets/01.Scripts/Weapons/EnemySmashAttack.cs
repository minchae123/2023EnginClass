using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySmashAttack : EnemyAttack
{
    [SerializeField] private Transform atkPosTrm;
    [SerializeField] private float atkRadius = 3f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask whatIsEnemy;

    public override void Attack(int damage, Vector3 targetVector)
    {
        Collider[] cols = Physics.OverlapSphere(atkPosTrm.position, atkRadius, whatIsEnemy);

        foreach(Collider c in cols)
        {
            if(c.TryGetComponent<IDamageable>(out IDamageable health))
            {
                Vector3 normal = atkPosTrm.position - c.transform.position;
                normal.y = 0;
                health.OnDamage(_damage, c.transform.position, normal);
            }
        }
    }

    public override void CancelAttack()
    {
    
    }

    public override void PreAttack()
    {
    
    }
}
