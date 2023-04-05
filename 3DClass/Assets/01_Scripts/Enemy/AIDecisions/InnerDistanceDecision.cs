using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerDistanceDecision : AIDecision
{
    [SerializeField] private float _distance = 5f;
    [SerializeField] private bool isAlwaysVisible = false;

    public override bool MakeADecision()
    {
        if (enemyController.TargetTrm == null) return false;

        float distance = Vector3.Distance(enemyController.TargetTrm.position, transform.position);

        if(distance < _distance)
        {
            aiActionData.LastSpot = enemyController.TargetTrm.position;
            aiActionData.TargetSpotted = true;
        }
        else
        {
            aiActionData.TargetSpotted = false;
        }
        return aiActionData.TargetSpotted;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
      if(UnityEditor.Selection.activeObject == gameObject || isActiveAndEnabled == true)
        {
            Color oldColor = Gizmos.color;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _distance);
            Gizmos.color = oldColor;
        }  
    }
#endif
}
