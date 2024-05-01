using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterActionSelectingState : MonsterBaseState
{
    public MonsterActionSelectingState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        monster.MonsterSelectAction();
    }
}
