using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Core;

public class PopupText : PoolableMono
{
    private TextMeshPro tmpText;

    private void Awake()
    {
        tmpText = GetComponent<TextMeshPro>();
    }

    public void StartPopup(string text, Vector3 pos, int fontSize, Color color, float time = 1f, float yDelta = 2f)
    {
        tmpText.SetText(text);
        tmpText.color = color;
        tmpText.fontSize = fontSize;
        transform.position = pos;
        StartCoroutine(ShowRoutine(time, yDelta));
    }

    private IEnumerator ShowRoutine(float time, float yDelta)
    {
        float percent = 0;
        float curTime = 0;
        Vector3 firstPos = transform.position;

        while(percent < 1)
        {
            curTime += Time.deltaTime;
            percent += curTime / time;
            float nextOpacity = Mathf.Lerp(1, 0, percent);

            float value = EaseInExpo(percent);

            transform.position = firstPos + new Vector3(0, yDelta * value, 0);
            tmpText.alpha = nextOpacity;

            Vector3 camDir = (transform.position - Define.MainCam.transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(camDir);
            yield return null;
        }
        PoolManager.Instance.Push(this);
    }

    public override void Init()
    {
        tmpText.alpha = 1;
    }

    private float EaseOutElastic(float x)
    {
        float c4 = (2f * Mathf.PI) / 3f;

        return x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x* 10 - 0.75f) * c4) + 1;
    }

    private float EaseInExpo(float x)
    {
        return x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10);
    }
}
