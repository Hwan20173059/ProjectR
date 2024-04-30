using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunAwayButton : MonoBehaviour
{
    BattleManager battleManager;

    Button button;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickRunAwayButton);
    }

    void OnClickRunAwayButton()
    {
        battleManager.OnClickRunAwayButton();
    }
}
