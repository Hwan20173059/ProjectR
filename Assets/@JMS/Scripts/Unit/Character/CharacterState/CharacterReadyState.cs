using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReadyState : CharacterBaseState
{
    public CharacterReadyState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("�ൿ �غ���");
    }

    public override void Update()
    {
        base.Update();
        character.CoolTimeUpdate();
    }
}
