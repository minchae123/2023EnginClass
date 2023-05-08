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

    private AgentController _controller;

    public void SetInit(AgentController controller)
    {
        _controller = controller;
    }

    public void CastDamage()
    {
        Vector3 startpos = transform.position - transform.forward * casterRadius;
        RaycastHit hit;
        bool isHit = Physics.SphereCast(startpos, casterRadius, transform.forward, out hit, casterRadius + casterInterpolation, targetLayer);
        if (isHit)
        {
            if(hit.collider.TryGetComponent<IDamageable>(out IDamageable health))
            {
                int damage = _controller.CharData.BaseDamage;
                float critical = _controller.CharData.BaseCritical;
                float criticalDamage = _controller.CharData.BaseCriticalDamage;

                float dice = Random.value;
                int fontSize = 10;
                Color fontColor = Color.white;

                if(dice < critical)
                {
                    damage = Mathf.CeilToInt(damage * criticalDamage);
                    fontSize = 15;
                    fontColor = Color.red;
                }

                health.OnDamage(damage, hit.point, hit.normal);

                PopupText pTxt = PoolManager.Instance.Pop("PopupText") as PopupText;
                pTxt.StartPopup(text: damage.ToString(), pos: hit.point + new Vector3(0, 2f, 0), fontSize: fontSize, color: fontColor);

            }
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
