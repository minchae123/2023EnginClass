using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEndDecision : AIDecision
{
    //���÷��� FieldInfo �� �����ͼ� ��������� ĳ���ϴ� �۾��� �ؾ���. 
    public override bool MakeADecision()
    {
        return _aiActionData.IsAttacking == false;
    }
}
