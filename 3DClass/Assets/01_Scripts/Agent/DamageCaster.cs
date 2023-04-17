using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    [Range(0.5f, 3f)]
    private float casterRadius = 1f;
    [SerializeField] private float casterInterpolation = 0.5f;
    [SerializeField] private LayerMask targetLayer;


    public void CastDamage()
    {
        Vector3 startpos = transform.position - transform.forward * casterRadius;
        RaycastHit hit;
        bool isHit = Physics.SphereCast(startpos, casterRadius, transform.forward, out hit, casterRadius + casterInterpolation, targetLayer);
        if (isHit)
        {
            Debug.Log($"�¾Ҵ� : {hit.collider.name}");
        }
        else
        {
            Debug.Log("�ȸ���");
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeGameObject == gameObject)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, casterRadius);
            Gizmos.color = Color.white;
        }
    }
#endif
}
