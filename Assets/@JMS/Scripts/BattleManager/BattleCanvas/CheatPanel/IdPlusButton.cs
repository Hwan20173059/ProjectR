using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdPlusButton : MonoBehaviour
{
    CheatPanel cheatPanel;

    Button button;

    public void Init(CheatPanel cheatPanel)
    {
        this.cheatPanel = cheatPanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickPlusButton);
    }

    void OnClickPlusButton()
    {
        ++cheatPanel.cheatId;
        if (cheatPanel.cheatId > DataManager.Instance.itemDatabase.equipDatas.Count - 1)
        {
            cheatPanel.cheatId = 0;
        }
        cheatPanel.text = cheatPanel.cheatId.ToString();
    }
}
