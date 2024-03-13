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

        battleManager.curStage++;

        if (battleManager.curStage == battleManager.Dungeons[battleManager.selectDungeon].Stages.Count)
        {
            battleCanvas.DungeonClearPanel.SetActive(true);
        }
        else
        {
            battleCanvas.NextStagePanel.SetActive(true);
        }
    }
}
