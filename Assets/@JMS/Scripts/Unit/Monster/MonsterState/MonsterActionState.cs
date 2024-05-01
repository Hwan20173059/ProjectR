using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonsterActionState : MonsterBaseState
{
    public MonsterActionState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        monster.MonsterAttack();
    }

}
