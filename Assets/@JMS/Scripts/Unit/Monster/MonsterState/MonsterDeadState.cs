using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class MonsterDeadState : MonsterBaseState
{
    public MonsterDeadState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        monster.MonsterDead();
    }

    public override void Exit()
    {
        base.Exit();

        monster.MonsterRevive();
    }
}
