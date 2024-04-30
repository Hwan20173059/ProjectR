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

        battleCanvas.UpdateBattleText("���� ����!");

        battleManager.BattleStart();
    }
}
