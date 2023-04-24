using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezeFeedback : Feedback
{
    [SerializeField] private float freezeTimeDelay = 0.05f, unFreezeTimeDealy = 0.02f;

    [SerializeField]
    [Range(0, 1f)]
    private float timeFreezeValue = 0.2f;

    public override void CreateFeedback()
    {
        FinishFeedback();
        TimeController.Instance.ModifyTimeScale(timeFreezeValue, freezeTimeDelay, () =>
        {
            TimeController.Instance.ModifyTimeScale(1, unFreezeTimeDealy);
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
