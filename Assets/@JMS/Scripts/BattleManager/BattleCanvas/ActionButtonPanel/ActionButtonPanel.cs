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
    RunAwayDisableButton runAwayDisableButton;
    ItemButton ItemButton;

    public void Init(BattleManager battlemanager)
    {
        battleManager = battlemanager;

        attackButton = GetComponentInChildren<AttackButton>();
        rouletteButton = GetComponentInChildren<RouletteButton>();
        runAwayButton = GetComponentInChildren<RunAwayButton>();
        runAwayDisableButton = GetComponentInChildren<RunAwayDisableButton>();
        ItemButton = GetComponentInChildren<ItemButton>();

        attackButton.button.onClick.AddListener(OnClickAttackButton);
        rouletteButton.button.onClick.AddListener(OnClickRouletteButton);
        runAwayButton.button.onClick.AddListener(OnClickRunAwayButton);
        ItemButton.button.onClick.AddListener(OnClickItemButton);

        rouletteButton.button.gameObject.SetActive(false);

        if (!PlayerManager.Instance.isDungeon)
            runAwayDisableButton.gameObject.SetActive(false);
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
        battleManager.battleCanvas.UseItemPanelOn();
    }
}
