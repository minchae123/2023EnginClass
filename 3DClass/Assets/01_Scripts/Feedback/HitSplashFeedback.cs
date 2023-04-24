using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitSplashFeedback : Feedback
{
    [SerializeField] private SplashEffectPlayer splashEffect;
    [SerializeField] private LayerMask whatIsGround;
    [SerializeField] private Color hitColor;

    private AIActionData aIActionData;

    private void Awake()
    {
        aIActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        SplashEffectPlayer effect = PoolManager.Instance.Pop(splashEffect.name) as SplashEffectPlayer;
        effect.transform.position = aIActionData.HitPoint;

        RaycastHit hit;
        if(Physics.Raycast(effect.transform.position, Vector3.down, out hit, 10f, whatIsGround))
        {
            effect.SetData(hitColor, -hit.distance, aIActionData.HitNormal);
            effect.StartPlay(3f);
        }
        else
        {
            Debug.Log("∆»¿Ã æ»¥Íæ∆ø‰");
        }
    }

    public override void FinishFeedback()
    {

    }
}
