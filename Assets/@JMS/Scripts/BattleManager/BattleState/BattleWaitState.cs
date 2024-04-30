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

        battleManager.BattleOverCheck();
    }

    public override void Update()
    {
        base.Update();

        battleManager.CheckPerformList();
    }
}
