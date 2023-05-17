using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissolveFeedback : Feedback
{
    [SerializeField]
    private SkinnedMeshRenderer _skinedMeshRenderer;

    private MaterialPropertyBlock _materialProp;

    public UnityEvent OnAfterDissolve = null; //������ ������ �� ��

    private readonly int _dissolveHeightHash = Shader.PropertyToID("_DissolveHeight");
    private readonly int _isDissolveHash = Shader.PropertyToID("_IsDissolve");

    protected void Awake()
    {
        _materialProp = new MaterialPropertyBlock();
        _skinedMeshRenderer.GetPropertyBlock(_materialProp);
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MaterialDissolve());
    }

    private IEnumerator MaterialDissolve()
    {
        float dissolveTime = 2f;
        float current = 0;
        float dissolveHeightStart = 5f;
        float dissolveHeightTarget = -5f;
        float dissolveHeight = 0;

        _materialProp.SetFloat(_isDissolveHash, 1); //true
        _skinedMeshRenderer.SetPropertyBlock(_materialProp);

        while(current < dissolveTime)
        {
            current += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(
                dissolveHeightStart, dissolveHeightTarget, current / dissolveTime);
            _materialProp.SetFloat(_dissolveHeightHash, dissolveHeight);
            _skinedMeshRenderer.SetPropertyBlock(_materialProp);
            yield return null;
        }

        OnAfterDissolve?.Invoke();
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();
        _materialProp.SetFloat(_isDissolveHash, 0); //true�� 1, false�� 0 ������ �ȴ�.
        _materialProp.SetFloat(_dissolveHeightHash, 5f); //5�� �ʱ�ȭ
        _skinedMeshRenderer.SetPropertyBlock(_materialProp);
    }
}
