using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePerformActionState : BattleBaseState
{
    public BattlePerformActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }
    public override void Exit()
    {
        base.Exit();
        if (!(character.IsDead))
        {
             // 캐릭터 상태를 이전 상태로 변경
            character.stateMachine.ChangeState(characterPrevState);
        }

        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
            {
                // 몬스터들의 상태를 이전 상태로 변경
                monsterPrevStateIndex = i;
                monsters[i].stateMachine.ChangeState(monsterPrevState);
            }
        }
    }
}
