using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitParticleFeedback : Feedback
{
    [SerializeField] private PoolableMono hitParticle;
    [SerializeField] private float effectPlayTime;
    private AIActionData aiActionData;

    private void Awake()
    {
        aiActionData = transform.parent.Find("AI").GetComponent<AIActionData>();
    }

    public override void CreateFeedback()
    {
        EffectPlayer effect = PoolManager.Instance.Pop(hitParticle.name) as EffectPlayer;
        effect.transform.position = aiActionData.HitPoint;
        effect.transform.rotation = Quaternion.LookRotation(aiActionData.HitNormal * -1);
        effect.StartPlay(effectPlayTime);
    }

    public override void FinishFeedback()
    {

    }
}
