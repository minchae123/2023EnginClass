using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DropDecal : PoolableMono
{
    private DecalProjector decalProjector;
    [SerializeField] private float defaultSize = 3;

    private readonly int alphaHash = Shader.PropertyToID("_Alpha");
    private readonly int sizeHash = Shader.PropertyToID("_Size");

    public override void Init()
    {
        decalProjector.material.SetFloat(alphaHash, 1);
    }

    private void Awake()
    {
        decalProjector = GetComponent<DecalProjector>();
        decalProjector.material = new Material(decalProjector.material);
    }

    private void Update()
    {
        /*if(Input.GetKeyDown(KeyCode.K))
        {
            SetUpSize(new Vector3(2, 2, 1));
            StartSeq(() => FadeOut(0.5f));
        }*/
    }

    public void StartSeq(Action EndCallback)
    {
        Material targetMat = decalProjector.material;
        targetMat.SetFloat(sizeHash, 0);

        DOTween.To(
            () => targetMat.GetFloat(sizeHash),
            value => targetMat.SetFloat(sizeHash, value),
            1,
            1f
            ).SetEase(Ease.InOutSine)
            .OnComplete(() => EndCallback());
    }

    public void FadeOut(float time)
    {
        Material targetMat = decalProjector.material;

        DOTween.To(
            () => targetMat.GetFloat(alphaHash),
            value => targetMat.SetFloat(alphaHash, value),
            0,
            time
            ).OnComplete(GotoPool);
    }

    public void GotoPool()
    {
        PoolManager.Instance.Push(this);
    }

    public void SetUpSize(Vector3 size)
    {
        decalProjector.size = size;
        defaultSize = size.x;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, defaultSize * 0.5f);
        Gizmos.color = Color.white;
    }
#endif
}