using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class NextStagePanel : MonoBehaviour
{
    BattleManager battleManager;

    NextStageButton nextStageButton;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        nextStageButton = GetComponentInChildren<NextStageButton>();

        nextStageButton.button.onClick.AddListener(NextStageStart);
    }

    void NextStageStart()
    {
        battleManager.NextStageStart();
    }
}
