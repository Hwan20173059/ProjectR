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

        if (stateMachine.BattleManager.Character.stateMachine.currentState is CharacterDeadState)
        {
            stateMachine.ChangeState(stateMachine.DefeatState);
        }
        if (stateMachine.BattleManager.isStageClear)
        {
            stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.WaitState);
            stateMachine.ChangeState(stateMachine.VictoryState);
        }
    }

    public override void Update()
    {
        base.Update();
        if(stateMachine.BattleManager.PerformList.Count > 0)
        {
            stateMachine.ChangeState(stateMachine.TakeActionState);
        }
    }
}
