using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleWaitState : BattleBaseState
{
    public BattleWaitState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
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
