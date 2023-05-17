using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSplashFeedback : Feedback
{
    [SerializeField]
    private SplashEffectPlayer _splashPrefab;
    [SerializeField]
    private LayerMask _whatIsGround;
    [SerializeField]
    private Color _hitColor;

    private AIActionData _aiActionData;

    private void Awake()
    {
        _aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        SplashEffectPlayer effect = PoolManager.Instance.Pop(_splashPrefab.name) as SplashEffectPlayer;
        effect.transform.position = _aiActionData.HitPoint;

        RaycastHit hit;

        if(Physics.Raycast(effect.transform.position, Vector3.down, out hit, 10f, _whatIsGround))
        {
            effect.SetData(_hitColor, -hit.distance, _aiActionData.HitNormal);
            effect.StartPlay(3f); //3���� ���ֱ�
        }else
        {
            Debug.Log("���� �ȴ�ƿ�");
        }
    }

    public override void FinishFeedback()
    {
        //�̰� �Ұ� ����
    }
}
