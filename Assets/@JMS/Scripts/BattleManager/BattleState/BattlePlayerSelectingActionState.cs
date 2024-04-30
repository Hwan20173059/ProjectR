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

        battleCanvas.UpdateBattleText($"�÷��̾��� ������ ��ٸ��� ���Դϴ�.\n\n(���� ������ ��� ���� : {3 - battleManager.useItemCount})");

        battleManager.ChangeMosterStateToWait();
    }

    public override void Exit()
    {
        base.Exit();

        battleManager.LoadMonsterState();
    }
}
