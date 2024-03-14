using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePlayerSelectingActionState : BattleBaseState
{
    public BattlePlayerSelectingActionState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        // 몬스터들의 현재 상태 저장
        for (int i = 0; i < monsters.Count; i++)
        {
            monsterPrevStateIndex = i;
            monsterPrevState = monsters[i].stateMachine.currentState;
        }
        // 몬스터들의 현재 상태 Wait으로 변경
        for (int i = 0; i < monsters.Count; i++)
        {
            if (!(monsters[i].IsDead))
                monsters[i].stateMachine.ChangeState(monsters[i].stateMachine.waitState);
        }
    }

    public override void Exit()
    {
        base.Exit();
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
