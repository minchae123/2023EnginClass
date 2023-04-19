using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DissolveFeedback : Feedback
{
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    private MaterialPropertyBlock materialProp;
    public UnityEvent OnAfterDissolve = null;

    private readonly int dissolveHeightHash = Shader.PropertyToID("_DissolveHeight");
    private readonly int isDissolveHash = Shader.PropertyToID("_isDissolve");

    protected void Awake()
    {
        materialProp = new MaterialPropertyBlock();
        skinnedMeshRenderer.GetPropertyBlock(materialProp);
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MaterialDissolve());
    }
    
    IEnumerator MaterialDissolve()
    {
        float dissolveTime = 2f;
        float cur = 0;
        float dissolveHeightStart = 5f;
        float dissolveHeightTarget = -5f;
        float dissolveHeight = 0;

        materialProp.SetFloat(isDissolveHash, 1);
        skinnedMeshRenderer.SetPropertyBlock(materialProp);

        while(cur < dissolveTime)
        {
            cur += Time.deltaTime;
            dissolveHeight = Mathf.Lerp(dissolveHeightStart, dissolveHeightTarget, cur / dissolveTime);
            materialProp.SetFloat(dissolveHeightHash, dissolveHeight);
            skinnedMeshRenderer.SetPropertyBlock(materialProp);
            yield return null;
        }

        OnAfterDissolve?.Invoke();
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();
        materialProp.SetFloat(isDissolveHash, 0);
        materialProp.SetFloat (dissolveHeightHash, 5f);
    }
}
