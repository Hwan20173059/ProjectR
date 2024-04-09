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

        if (battleManager.curStage == battleManager.stages.Count)
        {
            battleCanvas.DungeonClearPanelOn();
            GameEventManager.instance.questEvent.DungeonClear();

            Time.timeScale = 1f;
        }
        else
        {
            if (battleManager.IsAutoBattle)
                battleManager.NextStageStart();
            else
            {
                battleCanvas.NextStagePanelOn();
                RewardManager.instance.RewadPopup(battleManager.stages[battleManager.curStage - 1]);
            }
        }
    }
}
