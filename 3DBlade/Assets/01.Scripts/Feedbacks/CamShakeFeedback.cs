using UnityEngine;

public class CamShakeFeedback : Feedback
{
    [SerializeField]
    private float _shakePower, _shakeTime;


    public override void CreateFeedback()
    {
        CameraManager.Instance.AddShake(_shakePower, _shakeTime);
    }

    public override void FinishFeedback()
    {
        //�ƹ��͵� �Ұ� ����.
    }
}
