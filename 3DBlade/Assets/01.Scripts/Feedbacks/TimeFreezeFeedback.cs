using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeFeedback : Feedback
{
    [SerializeField]
    private float _freezeTimeDelay = 0.05f, _unFreezeTimeDelay = 0.02f;

    [SerializeField]
    [Range(0, 1f)]
    private float _timeFreezeValue = 0.2f;

    public override void CreateFeedback()
    {
        FinishFeedback();
        TimeController.Instance.ModifyTimeScale(_timeFreezeValue, _freezeTimeDelay, () =>
        {
            TimeController.Instance.ModifyTimeScale(1, _unFreezeTimeDelay);
        });
    }

    public override void FinishFeedback()
    {
        if (TimeController.Instance != null)
        {
            TimeController.Instance.ResetTimeScale();
        }
    }
}
