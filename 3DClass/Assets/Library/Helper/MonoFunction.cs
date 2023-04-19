using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoFunction : MonoBehaviour
{
    public static MonoFunction Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("MonoFunctio ¿À·ù");
        }
        Instance = this;
    }

    public void AddCoroutine(Action CallBack, float delayTime)
    {
        StartCoroutine(DelayCoroutine(CallBack, delayTime));
    }

    private IEnumerator DelayCoroutine(Action CallBack, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        CallBack?.Invoke();
    }
}
