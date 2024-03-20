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

        battleCanvas.BattleStateUpdate("플레이어의 선택을 기다리는 중입니다.");

        // 몬스터들의 현재 상태 저장
        for (int i = 0; i < monsters.Count; i++)
        {
            monstersPrevState[i] = monsters[i].stateMachine.currentState;
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
                monsters[i].stateMachine.ChangeState(monstersPrevState[i]);
            }
        }
    }
}
