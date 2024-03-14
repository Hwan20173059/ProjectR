using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterSelectActionState : MonsterBaseState
{
    public MonsterSelectActionState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        monster.selectAction = monster.actions[Random.Range(0, monster.actions.Count)];
        monster.curCoolTime = 0f;
        performList.Add(monster.monsterNumber);
        stateMachine.ChangeState(stateMachine.readyState);
    }
}
