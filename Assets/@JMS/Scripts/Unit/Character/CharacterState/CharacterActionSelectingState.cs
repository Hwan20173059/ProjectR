using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterActionSelectingState : CharacterBaseState
{
    public CharacterActionSelectingState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("�ൿ ������");

        battleManager.CharacterActionSelecting();
    }
    public override void Exit()
    {
        base .Exit();

        battleManager.CharacterSelectAction();
    }
}