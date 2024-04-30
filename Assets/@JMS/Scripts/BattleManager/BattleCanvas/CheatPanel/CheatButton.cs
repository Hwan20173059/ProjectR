using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class CheatButton : MonoBehaviour
{
    BattleManager battleManager;

    CheatPanel cheatPanel;

    Button button;

    public void Init(BattleManager battleManager, CheatPanel cheatPanel)
    {
        this.battleManager = battleManager;
        this.cheatPanel = cheatPanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickCheatButton);
    }

    void OnClickCheatButton()
    {
        battleManager.rouletteResult = RouletteResult.Cheat;
        battleManager.cheatItemId = cheatPanel.cheatId;
    }
}
