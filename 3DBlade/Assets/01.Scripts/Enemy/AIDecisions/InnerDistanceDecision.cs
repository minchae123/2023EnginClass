using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField]
    private float _distance = 5f;

    [SerializeField]
    private bool isAlwaysVisible = false;  //��� �����뵵
    
    public override bool MakeADecision()
    {
        if (_enemyController.TargetTrm == null) return false;

        float distance = Vector3.Distance(_enemyController.TargetTrm.position, transform.position);

        if(distance < _distance)  //�þ� ������ �������� ���� ����
        {
            _aiActionData.LastSpotPoint = _enemyController.TargetTrm.position; //���������� �� �������� ���
            _aiActionData.TargetSpotted = true; //���� �߰��ߴ�.
        }else
        {
            _aiActionData.TargetSpotted = false;
        }
        return _aiActionData.TargetSpotted;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(UnityEditor.Selection.activeObject == gameObject || isAlwaysVisible == true)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = oldColor;
        }
    }
#endif
}
