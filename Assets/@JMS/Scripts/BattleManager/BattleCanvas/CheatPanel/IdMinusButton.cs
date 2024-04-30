using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IdMinusButton : MonoBehaviour
{
    CheatPanel cheatPanel;

    Button button;

    public void Init(CheatPanel cheatPanel)
    {
        this.cheatPanel = cheatPanel;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickMinusButton);
    }

    void OnClickMinusButton()
    {
        --cheatPanel.cheatId;
        if (cheatPanel.cheatId < 0)
        {
            cheatPanel.cheatId = DataManager.Instance.itemDatabase.equipDatas.Count - 1;
        }
        cheatPanel.text = cheatPanel.cheatId.ToString();
    }
}
