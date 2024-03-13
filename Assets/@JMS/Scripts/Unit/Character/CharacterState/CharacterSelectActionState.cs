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

        character.IsSelectingAction = true;
    }
    public override void Exit()
    {
        base .Exit();

        character.IsSelectingAction = false;
    }
}