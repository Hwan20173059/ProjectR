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

        battleCanvas.battleDefeatPanel.SetActive(true);
    }
}
