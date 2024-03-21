using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterDeadState : CharacterBaseState
{
    public CharacterDeadState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();

        StateUpdate("행동 불능");

        character.hpBar.gameObject.SetActive(false);

        character.animator.SetBool("Dead", true);
    }

    public override void Exit()
    {
        base.Exit();

        character.hpBar.gameObject.SetActive(true);

        character.animator.SetBool("Dead", false);
    }
}
