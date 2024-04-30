using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattleOnButton : MonoBehaviour
{
    BattleManager battleManager;

    AutoBattlePanel autoBattlePanel;

    Button button;

    public void Init(BattleManager battleManager, AutoBattlePanel autoBattlePanel)
    {
        this.battleManager = battleManager;
        this.autoBattlePanel = autoBattlePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAutoBattleOnButton);
    }

    void OnClickAutoBattleOnButton()
    {
        autoBattlePanel.autoBattleOffButton.gameObject.SetActive(true);

        PlayerManager.Instance.autoBattle = false;

        if (autoBattlePanel.isSetting) { return; }

        battleManager.IsAutoBattle = false;
    }

}
