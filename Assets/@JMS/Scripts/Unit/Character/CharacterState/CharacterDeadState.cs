using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeadState : CharacterBaseState
{
    public CharacterDeadState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        character.Animator.SetBool("Dead", true);
    }

    public override void Exit()
    {
        base.Exit();

        character.Animator.SetBool("Dead", false);
    }
}
