using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterReadyState : MonsterBaseState
{
    public MonsterReadyState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Update()
    {
        base.Update();
        CoolTimeUpdate();
    }

    void CoolTimeUpdate()
    {
        if (stateMachine.Monster.curCoolTime < stateMachine.Monster.maxCoolTime)
        {
            stateMachine.Monster.curCoolTime += Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.SelectActionState);
        }
    }
}
