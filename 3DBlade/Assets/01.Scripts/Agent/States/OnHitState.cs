using Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHitState : CommonState
{
    [SerializeField]
    private float _recoverTime = 0.3f; //0.3�� ��Ŀ���� Ÿ��
    private float _hitTimer; 

    public override void OnEnterState()
    {
        _agentController.AgentMovementCompo.StopImmediately();
        _agentController.AgentHealthCompo.OnHitTriggered.AddListener(HandleHit);
    }

    public override void OnExitState()
    {
        _agentController.AgentHealthCompo.OnHitTriggered.RemoveListener(HandleHit);
        _animator.SetIsHit(false);
        _animator.SetHurtTrigger(false); 
    }

    public override bool UpdateState()
    {
        //�ǰݽð���ŭ �˹���ϸ鼭 �ִϸ��̼��� ����Ǿ�� ��
        _hitTimer += Time.deltaTime;
        //_agentController.AgentMovementCompo.IsActiveMove = fal
        if(_hitTimer >= _recoverTime)
        {
            _agentController.ChangeState(StateType.Normal); //�븻���·� ��ȯ
            return true;
        }
        return false;
    }
    private void HandleHit(int damage, Vector3 point, Vector3 normal)
    {
        normal.y = 0;
        _hitTimer = 0; //����
        _animator.SetIsHit(true);
        _animator.SetHurtTrigger(true);
        _agentController.transform.rotation = Quaternion.LookRotation(normal);
    }
}
