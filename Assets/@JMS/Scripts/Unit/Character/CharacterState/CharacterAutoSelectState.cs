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

        StateUpdate("�ൿ ������");

        battleManager.RouletteClear();
        battleManager.IsRouletteUsed = false;
        battleManager.IsSelectingAction = true;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.playerSelectingActionState);

        battleManager.CharacterAutoSelect();
    }

    public override void Exit()
    {
        base.Exit();

        battleManager.IsSelectingAction = false;
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}