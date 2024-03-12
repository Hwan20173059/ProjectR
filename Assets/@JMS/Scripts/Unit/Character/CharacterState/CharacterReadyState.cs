using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterReadyState : CharacterBaseState
{
    public CharacterReadyState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }
    public override void Update()
    {
        base.Update();
        CoolTimeUpdate();
    }

    void CoolTimeUpdate()
    {
        if(stateMachine.Character.curCoolTime < stateMachine.Character.maxCoolTime)
        {
            stateMachine.Character.curCoolTime += Time.deltaTime;
        }
        else
        {
            stateMachine.ChangeState(stateMachine.SelectActionState);
        }
    }
}