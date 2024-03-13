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
        if (monster.curCoolTime < monster.maxCoolTime)
        {
            monster.curCoolTime += Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.SelectActionState);
        }
    }
}
