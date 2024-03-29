using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoBattlePanel : MonoBehaviour
{
    BattleManager battleManager;

    AutoBattleOnButton autoBattleOnButton;
    AutoBattleOffButton autoBattleOffButton;
    x1OffButton x1OffButton;
    x2OffButton x2OffButton;
    x4OffButton x4OffButton;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

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
    }

    void OnClickAutoBattleOnButton()
    {
        autoBattleOffButton.gameObject.SetActive(true);

        battleManager.IsAutoBattle = false;
    }
    void OnClickAutoBattleOffButton()
    {
        autoBattleOffButton.gameObject.SetActive(false);

        battleManager.IsAutoBattle = true;

        if (battleManager.IsSelectingAction)
        {
            battleManager.CharacterAutoSelect();
        }
    }

    void OnClickx1OffButton()
    {
        Time.timeScale = 1f;

        x1OffButton.gameObject.SetActive(false);

        x2OffButton.gameObject.SetActive(true);
        x4OffButton.gameObject.SetActive(true);
    }

    void OnClickx2OffButton()
    {
        Time.timeScale = 2f;

        x2OffButton.gameObject.SetActive(false);

        x1OffButton.gameObject.SetActive(true);
        x4OffButton.gameObject.SetActive(true);
    }

    void OnClickx4OffButton()
    {
        Time.timeScale = 4f;

        x4OffButton.gameObject.SetActive(false);

        x1OffButton.gameObject.SetActive(true);
        x2OffButton.gameObject.SetActive(true);
    }
}
