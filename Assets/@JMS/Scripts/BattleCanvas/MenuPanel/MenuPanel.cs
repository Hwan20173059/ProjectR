using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : MonoBehaviour
{
    BattleCanvas battleCanvas;

    ContinueButton continueButton;
    SettingButton settingButton;

    public void Init(BattleCanvas battleCanvas)
    {
        this.battleCanvas = battleCanvas;

        continueButton = GetComponentInChildren<ContinueButton>();
        settingButton = GetComponentInChildren<SettingButton>();

        continueButton.button.onClick.AddListener(OnClickContinueButton);
        settingButton.button.onClick.AddListener(OnClickSettingButton);

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
}
