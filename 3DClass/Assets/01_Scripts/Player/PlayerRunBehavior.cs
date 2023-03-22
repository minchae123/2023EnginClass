using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunBehavior : StateMachineBehaviour
{
    private PlayerVFXManager vfxManager = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(vfxManager == null)
        {
            vfxManager = animator.transform.parent.GetComponent<PlayerVFXManager>();
        }
        vfxManager?.UpdateFootStep(true);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        vfxManager?.UpdateFootStep(false);
    }
}
