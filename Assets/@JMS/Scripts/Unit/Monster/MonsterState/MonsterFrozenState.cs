using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFrozenState : MonsterBaseState
{
    public MonsterFrozenState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Update()
    {
        base.Update();
        monster.BurnUpdate();
    }
}
