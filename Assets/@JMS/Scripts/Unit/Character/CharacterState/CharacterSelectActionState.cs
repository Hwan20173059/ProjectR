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
        battleManager.RouletteClear();
        battleManager.IsRouletteUsed = false;
        battleManager.IsSelectingAction = true;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.playerSelectingActionState);
    }
    public override void Exit()
    {
        base .Exit();

        battleManager.IsRouletteUsed = true;
        battleManager.IsSelectingAction = false;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}