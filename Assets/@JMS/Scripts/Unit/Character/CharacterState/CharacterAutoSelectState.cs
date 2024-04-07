using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAutoSelectState : CharacterBaseState
{
    public CharacterAutoSelectState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("행동 선택중");

        battleManager.battleCanvas.RouletteButtonOn();
        battleManager.IsRouletteUsed = false;
        battleManager.IsSelectingAction = true;
        battleManager.useItemCount = 0;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.playerSelectingActionState);

        battleManager.CharacterAutoSelect();
    }

    public override void Exit()
    {
        base.Exit();

        battleManager.IsSelectingAction = false;
        battleManager.useItemCount = 3;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}
