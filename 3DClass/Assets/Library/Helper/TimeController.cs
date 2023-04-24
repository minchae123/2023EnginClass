using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public static TimeController Instance;

    public void ResetTimeScale()
    {
        StopAllCoroutines();
        Time.timeScale = 1;
    }

    public void ModifyTimeScale(float end, float timeToWait, Action OnComplete = null)
    {
        StartCoroutine(TimeScaleCoroutine(end, timeToWait, OnComplete));
    }

    private IEnumerator TimeScaleCoroutine(float end, float timeToWait, Action OnComplete)
    {
        yield return new WaitForSecondsRealtime(timeToWait);
        Time.timeScale = end;
        OnComplete?.Invoke();
    }
}
