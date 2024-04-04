using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattleSettingPanel : MonoBehaviour
{
    AutoBattleOnButton autoBattleOnButton;
    AutoBattleOffButton autoBattleOffButton;
    x1OffButton x1OffButton;
    x2OffButton x2OffButton;
    x4OffButton x4OffButton;

    public void Init()
    {
        autoBattleOnButton = GetComponentInChildren<AutoBattleOnButton>();
        autoBattleOffButton = GetComponentInChildren<AutoBattleOffButton>();

        x1OffButton = GetComponentInChildren<x1OffButton>();
        x2OffButton = GetComponentInChildren<x2OffButton>();
        x4OffButton = GetComponentInChildren<x4OffButton>();

        autoBattleOnButton.button.onClick.AddListener(OnClickAutoBattleOnButton);
        autoBattleOffButton.button.onClick.AddListener(OnClickAutoBattleOffButton);

        x1OffButton.button.onClick.AddListener(OnClickx1OffButton);
        x2OffButton.button.onClick.AddListener(OnClickx2OffButton);
        x4OffButton.button.onClick.AddListener(OnClickx4OffButton);

        x1OffButton.gameObject.SetActive(false);

        if (PlayerManager.Instance.autoBattle)
            OnClickAutoBattleOffButton();

        switch (PlayerManager.Instance.battleSpeed)
        {
            case 1: OnClickx1OffButton(); break;
            case 2: OnClickx2OffButton(); break;
            case 4: OnClickx4OffButton(); break;
        }
    }

    void OnClickAutoBattleOnButton()
    {
        autoBattleOffButton.gameObject.SetActive(true);

        PlayerManager.Instance.autoBattle = false;
    }
    void OnClickAutoBattleOffButton()
    {
        autoBattleOffButton.gameObject.SetActive(false);

        PlayerManager.Instance.autoBattle = true;
    }

    void OnClickx1OffButton()
    {
        x1OffButton.gameObject.SetActive(false);

        x2OffButton.gameObject.SetActive(true);
        x4OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 1;
    }

    void OnClickx2OffButton()
    {
        x2OffButton.gameObject.SetActive(false);

        x1OffButton.gameObject.SetActive(true);
        x4OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 2;
    }

    void OnClickx4OffButton()
    {
        x4OffButton.gameObject.SetActive(false);

        x1OffButton.gameObject.SetActive(true);
        x2OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 4;
    }
}
