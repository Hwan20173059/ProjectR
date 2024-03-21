using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTakeActionState : BattleBaseState
{
    public BattleTakeActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        // 캐릭터 현재 상태 저장
        characterPrevState = character.stateMachine.currentState;
        // 몬스터들의 현재 상태 저장
        for (int i = 0; i < monsters.Count; i++)
        {
            monstersPrevState[i] = monsters[i].stateMachine.currentState;
        }
        // 캐릭터 현재 상태 Wait으로 변경
        if (!(character.IsDead))
            character.stateMachine.ChangeState(character.stateMachine.waitState);
        // 몬스터들의 현재 상태 Wait으로 변경
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.waitState);
        }
        // 수행 리스트에 가장 먼저 입력한 유닛의 상태를 Aciton으로 변경
        if (performList[0] == 100)
        {
            character.stateMachine.ChangeState(character.stateMachine.actionState);
        }
        else
        {
            monsters[performList[0]].stateMachine.ChangeState(monsters[performList[0]].stateMachine.actionState);
        }

        stateMachine.ChangeState(stateMachine.performActionState);
    }
}
