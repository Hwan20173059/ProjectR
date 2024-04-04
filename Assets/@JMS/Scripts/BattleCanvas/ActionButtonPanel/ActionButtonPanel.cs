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
    ItemButton ItemButton;

    public void Init(BattleManager battlemanager)
    {
        battleManager = battlemanager;

        attackButton = GetComponentInChildren<AttackButton>();
        rouletteButton = GetComponentInChildren<RouletteButton>();
        runAwayButton = GetComponentInChildren<RunAwayButton>();
        ItemButton = GetComponentInChildren<ItemButton>();

        attackButton.button.onClick.AddListener(OnClickAttackButton);
        rouletteButton.button.onClick.AddListener(OnClickRouletteButton);
        runAwayButton.button.onClick.AddListener(OnClickRunAwayButton);
        ItemButton.button.onClick.AddListener(OnClickItemButton);
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

    void OnClickItemButton()
    {
        battleManager.OnClickItemButton();
    }
}
