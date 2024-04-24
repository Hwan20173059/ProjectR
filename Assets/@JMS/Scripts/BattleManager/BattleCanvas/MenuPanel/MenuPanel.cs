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

        continueButton.button.onClick.AddListener(OnClickContinueButton);
        settingButton.button.onClick.AddListener(OnClickSettingButton);
        gameEndButton.button.onClick.AddListener(OnClickGameEndButton);

        gameObject.SetActive(false);
    }

    void OnClickContinueButton()
    {
        gameObject.SetActive(false);
    }
    void OnClickSettingButton()
    {
        battleCanvas.SettingsOn();
    }
    void OnClickGameEndButton()
    {
        Application.Quit();
    }
}
