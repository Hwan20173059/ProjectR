using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSelectActionState : CharacterBaseState
{
    public CharacterSelectActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("행동 선택중");

        battleManager.battleCanvas.RouletteButtonOn();
        battleManager.IsSelectingAction = true;
        battleManager.IsRouletteUsed = false;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.playerSelectingActionState);
    }
    public override void Exit()
    {
        base .Exit();

        battleManager.IsSelectingAction = false;
        battleManager.IsRouletteUsed = true;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}