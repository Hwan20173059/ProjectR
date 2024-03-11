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
        stateMachine.BattleManager.characterPrevState = stateMachine.BattleManager.Character.stateMachine.currentState;
        // 몬스터들의 현재 상태 저장
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            stateMachine.BattleManager.monstersPrevState[i] = stateMachine.BattleManager.Monsters[i].stateMachine.currentState;
        }
        // 캐릭터 현재 상태 Wait으로 변경
        stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.WaitState);
        // 몬스터들의 현재 상태 Wait으로 변경
        for (int i = 0; i < stateMachine.BattleManager.Monsters.Count; i++)
        {
            stateMachine.BattleManager.Monsters[i].stateMachine.ChangeState(stateMachine.BattleManager.Monsters[i].stateMachine.WaitState);
        }
        // 수행 리스트에 가장 먼저 입력한 유닛의 상태를 Aciton으로 변경
        if (stateMachine.BattleManager.PerformList[0] == 100)
        {
            stateMachine.BattleManager.Character.stateMachine.ChangeState(stateMachine.BattleManager.Character.stateMachine.ActionState);
        }
        else
        {
            stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.
                ChangeState(stateMachine.BattleManager.Monsters[stateMachine.BattleManager.PerformList[0]].stateMachine.ActionState);
        }
        // 수행 리스트에서 제거
        stateMachine.BattleManager.PerformList.RemoveAt(0);
        stateMachine.ChangeState(stateMachine.PerformActionState);
    }
}
