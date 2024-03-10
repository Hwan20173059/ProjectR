using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTakeActionState : BattleBaseState
{
    public BattleTakeActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        if (stateMachine.BattleManager.PerformList[0] == 100)
        {
            stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.ActionState);
        }
        else
        {
            stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.
                ChangeState(stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.ActionState);
        }

        stateMachine.BattleManager.PerformList.RemoveAt(0);
        stateMachine.ChangeState(stateMachine.PerformActionState);
    }
}
