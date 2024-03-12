using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDefeatState : BattleBaseState
{
    public BattleDefeatState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        stateMachine.ChangeState(stateMachine.EndState);
    }
    public override void Exit()
    {
        base.Exit();
    }
}
