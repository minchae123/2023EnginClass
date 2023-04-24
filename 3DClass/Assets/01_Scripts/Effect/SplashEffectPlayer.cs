using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SplashEffectPlayer : EffectPlayer
{
    private readonly int deltaYNameHash = Shader.PropertyToID("YDelta");
    private readonly int colorNameHash = Shader.PropertyToID("COLOR");
    private readonly int hitHormalHash = Shader.PropertyToID("HitNormal");

    public void SetData(Color color, float yDelta, Vector3 hitNormal)
    {
        foreach (VisualEffect ve in effects)
        {
            ve.SetFloat(deltaYNameHash, yDelta);
            ve.SetVector4(colorNameHash, color);
            ve.SetVector3(hitHormalHash, hitNormal);
        }
    }
}
