using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x2OffButton : MonoBehaviour
{
    AutoBattlePanel autoBattlePanel;

    Button button;

    public void Init(AutoBattlePanel autoBattlePanel)
    {
        this.autoBattlePanel = autoBattlePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickX2OffButton);
    }

    public void OnClickX2OffButton()
    {
        gameObject.SetActive(false);

        autoBattlePanel.x1OffButton.gameObject.SetActive(true);
        autoBattlePanel.x4OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 2;

        if (autoBattlePanel.isSetting) { return; }

        Time.timeScale = 2f;
    }

}
