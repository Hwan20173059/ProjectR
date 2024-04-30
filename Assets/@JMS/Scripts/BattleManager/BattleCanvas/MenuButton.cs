using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    BattleCanvas battleCanvas;

    Button button;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        button = GetComponent<Button>();
        button.onClick.AddListener(MenuPanelOn);
    }

    void MenuPanelOn()
    {
        battleCanvas.MenuPanelOn();
    }
}
