using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    BattleManager battleManager;

    Button button;

    public void Init(BattleManager battleManager)
    {
        this.battleManager = battleManager;

        button = GetComponent<Button>();
        button.onClick.AddListener(OnClickAttackButton);
    }

    void OnClickAttackButton()
    {
        battleManager.OnClickAttackButton();
    }
}
