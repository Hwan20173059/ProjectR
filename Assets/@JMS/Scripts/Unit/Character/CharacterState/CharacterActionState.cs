using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CharacterActionState : CharacterBaseState
{
    public CharacterActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateUpdate("°ø°ÝÁß");

        character.CharacterAttack();
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }

}
