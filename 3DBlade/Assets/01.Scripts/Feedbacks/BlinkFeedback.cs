using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkFeedback : Feedback
{
    [SerializeField]
    private SkinnedMeshRenderer _meshRenderer;
    [SerializeField]
    private float _blinkTime = 0.2f;

    private MaterialPropertyBlock _matPropBlock;

    private readonly int _blinkHash = Shader.PropertyToID("_Blink");

    private void Awake()
    {
        _matPropBlock = new MaterialPropertyBlock();
        _meshRenderer.GetPropertyBlock(_matPropBlock); //���⿡ �ش� ��Ƽ���� �ִ� ������Ƽ�� �������ش�.
    }

    public override void CreateFeedback()
    {
        StartCoroutine(MaterialBlink());
    }

    IEnumerator MaterialBlink()
    {
        _matPropBlock.SetFloat(_blinkHash, 0.5f);
        _meshRenderer.SetPropertyBlock(_matPropBlock);

        yield return new WaitForSeconds(_blinkTime);

        _matPropBlock.SetFloat(_blinkHash, 0);
        _meshRenderer.SetPropertyBlock(_matPropBlock);
    }

    public override void FinishFeedback()
    {
        StopAllCoroutines(); //�� ���غ��� �����ϴ� ��� �ڷ�ƾ ����
        _matPropBlock.SetFloat(_blinkHash, 0);
        _meshRenderer.SetPropertyBlock(_matPropBlock);

    }
}
