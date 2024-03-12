using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleVictoryState : BattleBaseState
{
    public BattleVictoryState(BattleStateMachine battleStateMachine) : base(battleStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        stateMachine.BattleManager.curStage++;

        if (stateMachine.BattleManager.curStage == stateMachine.BattleManager.Dungeons[stateMachine.BattleManager.selectDungeon].Stages.Count)
        {
            stateMachine.BattleManager.BattleCanvas.DungeonClearPanel.SetActive(true);
        }
        else
        {
            stateMachine.BattleManager.BattleCanvas.NextStagePanel.SetActive(true);
        }
    }
}
