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
        if(character == null)
        {
            battleManager.SpawnCharacter();
        }
        battleManager.SpawnMonster();
        stateMachine.ChangeState(stateMachine.waitState);
        battleManager.StartCoroutine(battleManager.BattleStart());
    }
}
