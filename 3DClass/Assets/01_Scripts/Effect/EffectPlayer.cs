using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EffectPlayer : PoolableMono
{
    [SerializeField] protected List<ParticleSystem> particles;
    [SerializeField] protected List<VisualEffect> effects;

    public void StartPlay(float endTime)
    {
        if (particles != null)
            particles.ForEach(p => p.Play());
        if (effects != null) 
            effects.ForEach(p => p.Play());

        StartCoroutine(Timer(endTime));
    }

    protected IEnumerator Timer(float timer)
    {
        yield return new WaitForSeconds(timer);
        PoolManager.Instance.Push(this);
    }

    public override void Reset()
    {
        if (particles != null)
            particles.ForEach(p =>
            {
                p.Simulate(0);
                p.Stop();
            });

        if (effects != null)
            effects.ForEach(p => p.Stop());
    }
}
