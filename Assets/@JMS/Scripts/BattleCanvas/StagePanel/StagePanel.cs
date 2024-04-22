using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    BattleManager battleManager;

    StageTMP stageText;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        stageText = GetComponentInChildren<StageTMP>();
    }

    public void SetStageText()
    {
        stageText.text = $"Stage {battleManager.curStage + 1} / {battleManager.stages.Count}";
    }
}
