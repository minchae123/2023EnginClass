using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField] private SkinnedMeshRenderer meshRenderer;
    [SerializeField] private float blinkTime = 0.2f;

    private MaterialPropertyBlock matPropBlock;

    private readonly int blinkHash = Shader.PropertyToID("_Blink");

    private void Awake()
    {
        matPropBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(matPropBlock); // 여기에 해당 메테리얼에 있는 프로퍼티 가져옴
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MaterialBlink());
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines();
        matPropBlock.SetFloat(blinkHash, 0);
        meshRenderer.SetPropertyBlock(matPropBlock);
    }

    IEnumerator MaterialBlink()
    {
        matPropBlock.SetFloat(blinkHash, 0.5f);
        meshRenderer.SetPropertyBlock(matPropBlock);

        yield return new WaitForSeconds(blinkTime);

        matPropBlock.SetFloat(blinkHash, 0);
        meshRenderer.SetPropertyBlock(matPropBlock);
    }
}
