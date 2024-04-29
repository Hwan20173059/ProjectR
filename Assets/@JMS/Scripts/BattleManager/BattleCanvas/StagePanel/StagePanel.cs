using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StagePanel : MonoBehaviour
{
    TextMeshProUGUI stageText;

    public void Init()
    {
        stageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void UpdateStageText(int curStage, int stageCount)
    {
        stageText.text = $"Stage {curStage + 1} / {stageCount}";
    }
}
