using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    BattleManager battleManager;

    Button button;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickItemButton);
    }

    void OnClickItemButton()
    {
        battleManager.battleCanvas.UseItemPanelOn();
    }
}