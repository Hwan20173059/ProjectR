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
        RewardManager.instance.AddReward(battleManager.stages[battleManager.curStage - 1]);
        if (battleManager.curStage == battleManager.stages.Count)
        {
            RewardManager.instance.RewardPopup();
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
            }
        }
    }
}
