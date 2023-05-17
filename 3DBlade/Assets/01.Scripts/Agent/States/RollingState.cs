using UnityEngine;

public class RollingState : CommonState
{
    [SerializeField]
    private float _rollingSpeed = 0.4f, _animationThreshold = 0.1f;  //�ִϸ��̼� ������Ȧ��

    private float _timer = 0;
    public override void OnEnterState()
    {
        _animator.OnAnimationEndTrigger += RollingEndHandle;
        _animator.SetRollingState(true);
        _agentMovement.IsActiveMove = false;
        //���⼭ ��� ������ ���� ȸ����ų�� �����ؾ� �Ѵ�. ������ ���콺�� ������ �غ���.
        //Vector3 mousePos = _agentInput.GetMouseWorldPosition();
        //Vector3 dir = mousePos - _agentController.transform.position;

        //_agentMovement.SetRotation(mousePos);

        Vector3 keyDir = _agentInput.GetCurrentInputDirection();
        //���⼭ �޾Ҵµ� ���� Ű�� �ȴ����� �־��ٸ� ���� �ٶ󺸴� �������� �Ѹ�����.
        if(keyDir.magnitude < 0.1f)
        {
            keyDir = _agentController.transform.forward;
        }
        _agentMovement.SetRotation(keyDir + _agentController.transform.position);
        _agentMovement.SetMovementVelocity(keyDir.normalized * _rollingSpeed);
        _timer = 0; //Ÿ�̸� ����
    }
    
    public override void OnExitState()
    {
        _animator.OnAnimationEndTrigger -= RollingEndHandle;
        _agentMovement.IsActiveMove = true;
        _animator.SetRollingState(false);
    }
    private void RollingEndHandle()
    {
        if (_timer < _animationThreshold) return;

        _agentMovement.StopImmediately();
        _agentController.ChangeState(Core.StateType.Normal);
    }

    public override bool UpdateState()
    {
        _timer += Time.deltaTime;
        return true;
    }
}