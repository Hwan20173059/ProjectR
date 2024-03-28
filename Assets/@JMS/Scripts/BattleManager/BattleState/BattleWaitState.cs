using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWaitState : BattleBaseState
{
    public BattleWaitState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (character.IsDead)
        {
            stateMachine.ChangeState(stateMachine.defeatState);
        }
        if (stateMachine.battleManager.StageClearCheck())
        {
            character.stateMachine.ChangeState(character.stateMachine.waitState);
            stateMachine.ChangeState(stateMachine.victoryState);
        }
    }

    public override void Update()
    {
        base.Update();
        if(performList.Count > 0)
        {
            stateMachine.ChangeState(stateMachine.takeActionState);
        }
    }
}
