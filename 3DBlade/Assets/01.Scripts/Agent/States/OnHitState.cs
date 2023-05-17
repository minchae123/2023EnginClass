using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitState : CommonState
{
    [SerializeField] private float recoverTime = 0.3f;
    private float hitTimer;

    public override void OnEnterState()
    {
        _agentController.AgentMovementCompo.StopImmediately();
        _agentController.AgentHelathCompo.OnHitTriggered.AddListener(HandleHit);
    }

    public override void OnExitState()
    {
        _agentController.AgentHelathCompo.OnHitTriggered.RemoveListener(HandleHit);
        _animator.SetIsHit(false);
        _animator.SetHurtTrigger(false);
    }

    public override bool UpdateState()
    {
        hitTimer += Time.deltaTime;
        
        if (hitTimer >= recoverTime)
        {
            _agentController.ChangeState(StateType.Normal);
            return true;
        }
        return false;
    }

    private void HandleHit(int damage, Vector3 point, Vector3 normal)
    {
        normal.y = 0;
        hitTimer = 0;
        _animator.SetIsHit(true);
        _animator.SetHurtTrigger(true);
        _agentController.transform.rotation = Quaternion.LookRotation(normal);
    }
}
