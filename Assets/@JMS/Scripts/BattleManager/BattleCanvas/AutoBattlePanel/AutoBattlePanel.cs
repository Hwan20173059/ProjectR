using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattlePanel : MonoBehaviour
{
    public AutoBattleOffButton autoBattleOffButton;
    public AutoBattleOnButton autoBattleOnButton;
    public x1OffButton x1OffButton;
    public x2OffButton x2OffButton;
    public x4OffButton x4OffButton;

    public bool isSetting;

    public void Init(BattleManager battleManager, bool isSetting)
    {
        this.isSetting = isSetting;

        autoBattleOffButton = GetComponentInChildren<AutoBattleOffButton>();
        autoBattleOnButton = GetComponentInChildren<AutoBattleOnButton>();

        autoBattleOffButton.Init(battleManager, this);
        autoBattleOnButton.Init(battleManager, this);

        x1OffButton = GetComponentInChildren<x1OffButton>();
        x2OffButton = GetComponentInChildren<x2OffButton>();
        x4OffButton = GetComponentInChildren<x4OffButton>();

        x1OffButton.Init(this);
        x2OffButton.Init(this);
        x4OffButton.Init(this);

        switch (PlayerManager.Instance.battleSpeed)
        {
            case 1: OnClickX1OffButton(); break;
            case 2: OnClickX2OffButton(); break;
            case 4: OnClickX4OffButton(); break;
        }
    }

    void OnClickX1OffButton()
    {
        x1OffButton.OnClickX1OffButton();
    }

    void OnClickX2OffButton()
    {
        x2OffButton.OnClickX2OffButton();
    }

    void OnClickX4OffButton()
    {
        x4OffButton.OnClickX4OffButton();
    }
}
