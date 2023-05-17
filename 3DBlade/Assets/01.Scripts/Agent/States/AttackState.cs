using Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : CommonState
{
    public event Action<int> OnAttackStart = null;
    public event Action OnAttackEnd = null;

    [SerializeField]
    private float _keyDelay = 0.5f;

    private int _currentCombo = 1; //���� �޺��� ������ 
    private bool _canAttack = true; // ���Է� ���� ���ؼ� ���� ���ݰ��ɻ����ΰ�
    private float _keyTimer = 0; //���������� �̷�������� ��ٸ��� �ð�
    //�� �ð����� �Է� ���ϸ� idle�� ���ư���.

    private float _attackStartTime; //������ ���۵� �ð��� ����ϰ�
    [SerializeField]
    private float _attackSlideDuration = 0.2f, _attackSlideSpeed = 0.1f;
    //�̰Ŵ� �����̵�Ǵ� �ð��� �����̵� �Ǵ� ���ǵ带 ��Ÿ����.

    private DamageCaster _damageCaster;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _damageCaster = agentRoot.Find("DamageCaster").GetComponent<DamageCaster>();
    }

    public override void OnEnterState()
    {
        _agentInput.OnAttackKeyPress += OnAttackHandle;
        _animator.OnAnimationEndTrigger += OnAnimationEnd;
        _agentInput.OnRollingKeyPress += OnRollingHandle;
        _animator.OnAnimationEventTrigger += OnDamageCastHandle;

        _currentCombo = 0;
        _canAttack = true;
        _animator.SetAttackState(true);

        _agentMovement.IsActiveMove = false;  //Ű���� �̵��� ��װ�
        Vector3 pos = _agentInput.GetMouseWorldPosition();
        _agentMovement.SetRotation(pos);
        OnAttackHandle(); //ó�� 1Ÿ ������
    }

    public override void OnExitState() 
    {
        _agentInput.OnAttackKeyPress -= OnAttackHandle;
        _animator.OnAnimationEndTrigger -= OnAnimationEnd;
        _agentInput.OnRollingKeyPress -= OnRollingHandle;
        _animator.OnAnimationEventTrigger -= OnDamageCastHandle;

        _animator.SetAttackState(false);
        _animator.SetAttackTrigger(false);

        _agentMovement.IsActiveMove = true; //Ű���� �̵��� Ǯ���ְ�

        OnAttackEnd?.Invoke();
    }

    private void OnDamageCastHandle()
    {
        _damageCaster.CastDamage();
    }

    private void OnRollingHandle()
    {
        _agentController.ChangeState(StateType.Rolling);
    }

    private void OnAnimationEnd()
    {
        _canAttack = true;
        _keyTimer = _keyDelay; //0.5�� ��ٸ��� ����
    }


    public void OnAttackHandle()
    {
        if(_canAttack && _currentCombo < 3)
        {
            _attackStartTime = Time.time;
            _canAttack = false;
            _currentCombo++;
            //�ִϸ��̼� Ʈ���� ���ְ�
            _animator.SetAttackTrigger(true);
            OnAttackStart?.Invoke(_currentCombo); //���� �޺���ġ�� �������ش�.
        }
    }

    public override bool UpdateState()
    {
        if(Time.time < _attackStartTime + _attackSlideDuration) //�����̵尡 �ǰ� �־�� �ϴ� �ð�
        {
            float timePassed = Time.time - _attackStartTime; //���� �귯�� �ð��� ������
            float lerpTime = timePassed / _attackSlideDuration; //0~1������ �����ϰ�

            Vector3 targetSpeed = Vector3.Lerp(_agentController.transform.forward * _attackSlideSpeed, 
                                                Vector3.zero, 
                                                lerpTime);
            _agentMovement.SetMovementVelocity(targetSpeed);
        }

        if(_canAttack && _keyTimer > 0)
        {
            _keyTimer -= Time.deltaTime;
            if(_keyTimer <= 0)
            {
                _agentController.ChangeState(StateType.Normal);
            }
        }

        return true;
    }
}
