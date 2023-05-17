using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeCameraFeedback : Feedback
{
    [SerializeField] private float shakePower, shakeTime;

    public override void CreateFeedback()
    {
        CameraManager.Instance.AddShake(shakePower, shakeTime);    
    }

    public override void FinishFeedback()
    {

    }
}
