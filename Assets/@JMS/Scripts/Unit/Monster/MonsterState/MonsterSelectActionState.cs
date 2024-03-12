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
        stateMachine.Monster.selectAction = stateMachine.Monster.actions[Random.Range(0, stateMachine.Monster.actions.Count)];
        stateMachine.Monster.battleManager.PerformList.Add(stateMachine.Monster.monsterNumber);
        stateMachine.ChangeState(stateMachine.ReadyState);
    }
}
