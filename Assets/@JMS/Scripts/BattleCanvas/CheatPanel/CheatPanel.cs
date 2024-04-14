using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatPanel : MonoBehaviour
{
    BattleManager battleManager;

    CheatButton cheatButton;
    CheatIdTMP cheatIdTMP;
    IdMinusButton idMinusButton;
    IdPlusButton idPlusButton;

    int itemId = 0;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        cheatButton = GetComponentInChildren<CheatButton>();
        cheatIdTMP = GetComponentInChildren<CheatIdTMP>();
        idMinusButton = GetComponentInChildren<IdMinusButton>();
        idPlusButton = GetComponentInChildren<IdPlusButton>();

        cheatButton.button.onClick.AddListener(OnClickCheatButton);
        idMinusButton.button.onClick.AddListener(OnClickMinusButton);
        idPlusButton.button.onClick.AddListener(OnClickPlusButton);
    }

    void OnClickCheatButton()
    {
        battleManager.rouletteResult = RouletteResult.Cheat;
        battleManager.cheatItemId = itemId;
    }

    void OnClickMinusButton()
    {
        --itemId;
        if (itemId < 0)
        {
            itemId = DataManager.Instance.itemDatabase.equipDatas.Count - 1;
        }
        cheatIdTMP.text = itemId.ToString();
    }

    void OnClickPlusButton()
    {
        ++itemId;
        if (itemId > DataManager.Instance.itemDatabase.equipDatas.Count - 1)
        {
            itemId = 0;
        }
        cheatIdTMP.text = itemId.ToString();
    }
}
