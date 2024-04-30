using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class x4OffButton : MonoBehaviour
{
    AutoBattlePanel autoBattlePanel;

    Button button;

    public void Init(AutoBattlePanel autoBattlePanel)
    {
        this.autoBattlePanel = autoBattlePanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickX4OffButton);
    }

    public void OnClickX4OffButton()
    {
        gameObject.SetActive(false);

        autoBattlePanel.x1OffButton.gameObject.SetActive(true);
        autoBattlePanel.x2OffButton.gameObject.SetActive(true);

        PlayerManager.Instance.battleSpeed = 4;

        if (autoBattlePanel.isSetting) { return; }
        
        Time.timeScale = 4f;
    }

}
