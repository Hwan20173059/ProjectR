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

        monster.CoolTimeUpdate();
        monster.BurnUpdate();
    }

}
