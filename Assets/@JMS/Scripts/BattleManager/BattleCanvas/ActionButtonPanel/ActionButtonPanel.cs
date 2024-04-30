using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class ActionButtonPanel : MonoBehaviour
{
    AttackButton attackButton;
    RouletteButton rouletteButton;
    RunAwayButton runAwayButton;
    RunAwayDisableButton runAwayDisableButton;
    ItemButton ItemButton;

    public void Init(BattleManager battleManager)
    {
        attackButton = GetComponentInChildren<AttackButton>();
        rouletteButton = GetComponentInChildren<RouletteButton>();
        ItemButton = GetComponentInChildren<ItemButton>();
        runAwayButton = GetComponentInChildren<RunAwayButton>();

        attackButton.Init(battleManager);
        rouletteButton.Init(battleManager);
        ItemButton.Init(battleManager);
        runAwayButton.Init(battleManager);

        runAwayDisableButton = GetComponentInChildren<RunAwayDisableButton>();
        if (!PlayerManager.Instance.isDungeon)
            runAwayDisableButton.gameObject.SetActive(false);

    }

    public void RouletteButtonOn()
    {
        rouletteButton.RouletteButtonOn();
    }
    public void RouletteButtonOff()
    {
        rouletteButton.RouletteButtonOff();
    }

}
