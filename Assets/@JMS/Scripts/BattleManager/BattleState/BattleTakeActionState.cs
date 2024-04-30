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

        battleManager.PerformAction();
    }

}
