using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActionButtonPanel : MonoBehaviour
{
    BattleManager battleManager;

    AttackButton attackButton;
    public RouletteButton rouletteButton;
    RunAwayButton runAwayButton;

    public void Init(BattleManager battlemanager)
    {
        battleManager = battlemanager;

        attackButton = GetComponentInChildren<AttackButton>();
        rouletteButton = GetComponentInChildren<RouletteButton>();
        runAwayButton = GetComponentInChildren<RunAwayButton>();

        attackButton.button.onClick.AddListener(OnClickAttackButton);
        rouletteButton.button.onClick.AddListener(OnClickRouletteButton);
        runAwayButton.button.onClick.AddListener(OnClickRunAwayButton);
    }

    void OnClickAttackButton()
    {
        battleManager.OnClickAttackButton();
    }

    void OnClickRouletteButton()
    {
        battleManager.OnClickRouletteButton();
    }

    void OnClickRunAwayButton()
    {
        battleManager.OnClickRunAwayButton();
    }
}
