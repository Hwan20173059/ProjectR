using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActionButtonPanel : MonoBehaviour
{
    BattleManager battleManager;

    AttackButton attackButton;
    public RouletteButton rouletteButton;

    public void Init(BattleManager battlemanager)
    {
        battleManager = battlemanager;

        attackButton = GetComponentInChildren<AttackButton>();
        rouletteButton = GetComponentInChildren<RouletteButton>();

        attackButton.button.onClick.AddListener(OnClickAttackButton);
        rouletteButton.button.onClick.AddListener(OnClickRouletteButton);
    }

    void OnClickAttackButton()
    {
        battleManager.OnClickAttackButton();
    }

    void OnClickRouletteButton()
    {
        battleManager.OnClickRouletteButton();
    }
}
