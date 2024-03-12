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
            //stateMachine.Character.ActionBar.transform.localScale = new Vector3
            //    (Mathf.Clamp(stateMachine.Character.curCoolTime / stateMachine.Character.maxCoolTime, 0, stateMachine.Character.maxCoolTime), 1, 1);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.SelectActionState);
        }
    }
}
