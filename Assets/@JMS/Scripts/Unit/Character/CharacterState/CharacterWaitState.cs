using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWaitState : CharacterBaseState
{
    public CharacterWaitState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("�����");
    }
}
