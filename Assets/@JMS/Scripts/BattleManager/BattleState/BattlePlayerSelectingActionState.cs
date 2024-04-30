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

        battleCanvas.UpdateBattleText($"플레이어의 선택을 기다리는 중입니다.\n\n(남은 아이템 사용 갯수 : {3 - battleManager.useItemCount})");

        battleManager.ChangeMosterStateToWait();
    }

    public override void Exit()
    {
        base.Exit();

        battleManager.LoadMonsterState();
    }
}
