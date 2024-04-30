using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class MenuPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;

    ContinueButton continueButton;
    SettingButton settingButton;
    GameEndButton gameEndButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        continueButton = GetComponentInChildren<ContinueButton>();
        settingButton = GetComponentInChildren<SettingButton>();
        gameEndButton = GetComponentInChildren<GameEndButton>();

        continueButton.Init(this);
        settingButton.Init(this);
        gameEndButton.Init();

        gameObject.SetActive(false);
    }

    public void OnClickSettingButton()
    {
        battleCanvas.SettingsOn();
    }

}
