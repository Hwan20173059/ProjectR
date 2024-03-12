using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartState : BattleBaseState
{
    public BattleStartState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(stateMachine.BattleManager.Character == null)
        {
            stateMachine.BattleManager.SpawnCharacter();
        }
        stateMachine.BattleManager.SpawnMonster();
        stateMachine.ChangeState(stateMachine.WaitState);
        stateMachine.BattleManager.StartCoroutine(stateMachine.BattleManager.BattleStart());
    }
}
