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
        // 캐릭터 상태를 이전 상태로 변경
        stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.characterPrevState);
        // 몬스터들의 상태를 이전 상태로 변경
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            stateMachine.BattleManager.Monsters[i].stateMachine.ChangeState(stateMachine.BattleManager.monstersPrevState[i]);
        }
    }
}
