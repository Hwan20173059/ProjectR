using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x1OffButton : MonoBehaviour
{
    AutoBattlePanel autoBattlePanel;

    Button button;

    public void Init(AutoBattlePanel autoBattlePanel)
    {
        this.autoBattlePanel = autoBattlePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickX1OffButton);

        gameObject.SetActive(false);
    }

    public void OnClickX1OffButton()
    {
        gameObject.SetActive(false);

        autoBattlePanel.x2OffButton.gameObject.SetActive(true);
        autoBattlePanel.x4OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 1;

        if (autoBattlePanel.isSetting) { return; }

        Time.timeScale = 1f;
    }

}
