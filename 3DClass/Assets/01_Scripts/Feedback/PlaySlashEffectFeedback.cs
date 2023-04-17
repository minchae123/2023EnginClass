using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySlashEffectFeedback : Feedback
{
    [SerializeField] private PoolableMono slashEffect;
    [SerializeField] private float effectPlayTime;
    private AIActionData aiActionData;

    private void Awake()
    {
        aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(slashEffect.name) as EffectPlayer;
        effect.transform.position = aiActionData.HitPoint;
        effect.StartPlay(effectPlayTime);
    }

    public override void FinishFeedback()
    {

    }
}
