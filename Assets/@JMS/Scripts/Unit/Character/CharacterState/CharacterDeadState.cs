using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
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

        StateUpdate("�ൿ �Ҵ�");

        character.CharacterDead();
    }

    public override void Exit()
    {
        base.Exit();

        character.CharacterRevive();
    }
}
