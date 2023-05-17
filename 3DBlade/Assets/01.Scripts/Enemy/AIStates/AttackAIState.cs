using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackAIState : CommonAIState
{
    [SerializeField]
    private float _rotateSpeed = 720f;

    protected Vector3 _targetVector; //���� �ٶ󺸴� ����

    private bool _isActive = false;

    private int _atkDamage = 1;
    private float _atkMotionDelay = 0.2f;

    [SerializeField]
    private float _atkCoolTime = 1f;
    private float _lastAtkTime;

    public override void SetUp(Transform agentRoot)
    {
        base.SetUp(agentRoot);
        _rotateSpeed = _enemyController.EnemyData.RotateSpeed;
        _atkDamage = _enemyController.EnemyData.AtkDamage;
        _atkMotionDelay = _enemyController.EnemyData.AtkCoolTime;
    }

    public override void OnEnterState()
    {
        _enemyController.NavMovement.StopImmediately();
        _enemyController.AgentAnimator.OnAnimationEventTrigger += AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger += AttackAnimationEndHandle;
        _enemyController.AgentAnimator.OnPreAnimationEventTrigger += PreAttackHandle;
        _aiActionData.IsAttacking = false;

        _isActive = true;
    }

    public override void OnExitState()
    {
        _enemyController.AgentAnimator.OnAnimationEventTrigger -= AttackCollisionHandle;
        _enemyController.AgentAnimator.OnAnimationEndTrigger -= AttackAnimationEndHandle;
        _enemyController.AgentAnimator.OnPreAnimationEventTrigger -= PreAttackHandle;

        _enemyController.AgentAnimator.SetAttackState(false);  //�ִϸ��̼� ����
        _enemyController.AgentAnimator.SetAttackTrigger(false);

        _enemyController.CancelAttack(); //������ ���

        _isActive = false;
    }

    private void PreAttackHandle()
    {
        _enemyController.PreAttack();
    }

    //���� �ִϸ��̼��� ������ �� ó��
    private void AttackAnimationEndHandle()
    {
        //_aiActionData.IsAttacking = false;
        _enemyController.AgentAnimator.SetAttackState(false);

        _lastAtkTime = Time.time;

        MonoFunction.Instance.AddCoroutine(() =>
        {
            _aiActionData.IsAttacking = false;
        }, _atkMotionDelay);        
    }

    private void AttackCollisionHandle()
    {
        _enemyController.AttackWeapon(_atkDamage, _targetVector);
    }

    public override bool UpdateState()
    {
        if( base.UpdateState())
        {
            return true;
        }
        
        if (_aiActionData.IsAttacking == false && _isActive)
        {
            SetTarget(); //Ÿ���� ���ϵ��� ���� ������ְ�
                         //���⼭ ���� �����̼� ���ǵ忡 ���� ���ƾ� �ϴµ� 

            Vector3 currentFrontVector = transform.forward; //ĳ������ �������� 
            float angle = Vector3.Angle(currentFrontVector, _targetVector);

            if (angle >= 10f)
            {
                Vector3 result = Vector3.Cross(currentFrontVector, _targetVector);

                float sign = result.y > 0 ? 1 : -1;
                _enemyController.transform.rotation =
                    Quaternion.Euler(0, sign * _rotateSpeed * Time.deltaTime, 0) * _enemyController.transform.rotation;
            }
            else if(_lastAtkTime + _atkCoolTime < Time.time )
            {
                _aiActionData.IsAttacking = true;
                _enemyController.AgentAnimator.SetAttackState(true);
                _enemyController.AgentAnimator.SetAttackTrigger(true); //���ݸ���� ����Ѵ�.
            }
        }
        return false;
    }

    private void SetTarget()
    {
        _targetVector = _enemyController.TargetTrm.position - transform.position;
        _targetVector.y = 0; //���̴� ���ٰ� ����
    }
}
