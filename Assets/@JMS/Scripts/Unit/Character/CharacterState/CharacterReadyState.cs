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

        StateUpdate("행동 준비중");
    }

    public override void Update()
    {
        base.Update();
        CoolTimeUpdate();
    }

    void CoolTimeUpdate()
    {
        if(character.curCoolTime < character.maxCoolTime)
        {
            character.curCoolTime += Time.deltaTime;
            battleManager.battleCanvas.UpdateActionBar();
        }
        else
        {
            if (battleManager.IsAutoBattle)
                stateMachine.ChangeState(stateMachine.autoSelectState);
            else
                stateMachine.ChangeState(stateMachine.selectActionState);
        }
    }
}
