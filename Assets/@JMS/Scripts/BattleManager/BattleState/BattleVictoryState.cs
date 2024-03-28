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

        if (battleManager.curStage == battleManager.dungeonList[battleManager.selectDungeon].stages.Count)
        {
<<<<<<< HEAD
            battleCanvas.dungeonClearPanel.SetActive(true);
            GameEventManager.instance.battleEvent.DungeonClear();
=======
            battleCanvas.DungeonClearPanelOn();
            GameEventManager.instance.questEvent.DungeonClear();
>>>>>>> origin/Dev_Branch
        }
        else
        {
            battleCanvas.NextStagePanelOn();
        }
    }
}
