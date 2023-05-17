using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private CinemachineVirtualCamera followCam;
    private CinemachineBasicMultiChannelPerlin camPerlin;

    private float initPower;
    private float initTime;
    private float curShakeTime;

    public void Init(Transform cameraTrm)
    {
        followCam = cameraTrm.GetComponent<CinemachineVirtualCamera>();
        camPerlin = followCam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void AddShake(float power, float time)
    {
        camPerlin.m_AmplitudeGain = initPower = power;
        curShakeTime = initTime = time;
    }

    private void Update()
    {
        if(curShakeTime > 0)
        {
            curShakeTime -= Time.deltaTime;

            float ampGain = Mathf.Lerp(initPower, 0, curShakeTime / initTime);
            camPerlin.m_AmplitudeGain = ampGain;

            if(curShakeTime < 0)
            {
                curShakeTime = 0;
                camPerlin.m_AmplitudeGain = 0;
            }
        }
    }

}
