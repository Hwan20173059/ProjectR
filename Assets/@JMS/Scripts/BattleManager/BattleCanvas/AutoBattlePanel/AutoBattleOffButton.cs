using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoBattleOffButton : MonoBehaviour
{
    BattleManager battleManager;

    AutoBattlePanel autoBattlePanel;

    Button button;

    public void Init(BattleManager battleManager, AutoBattlePanel autoBattlePanel)
    {
        this.battleManager = battleManager;
        this.autoBattlePanel = autoBattlePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAutoBattleOffButton);

        if (PlayerManager.Instance.autoBattle)
            OnClickAutoBattleOffButton();
    }

    void OnClickAutoBattleOffButton()
    {
        gameObject.SetActive(false);

        PlayerManager.Instance.autoBattle = true;

        if (autoBattlePanel.isSetting) { return; }

        battleManager.IsAutoBattle = true;

        if (battleManager.IsSelectingAction)
        {
            battleManager.CharacterAutoSelect();
        }
    }

}
