using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleCanvas : MonoBehaviour
{
    private BattleManager battleManager;
    private Character character { get { return battleManager.character; } set { battleManager.character = value; } }
    private Monster selectMonster { get { return battleManager.selectMonster; } set { battleManager.selectMonster = value; } }

    CharacterStatePanel characterStatePanel;
    ActionButtonPanel actionButtonPanel;
    RoulettePanel roulettePanel;
    NextStagePanel nextStagePanel;
    BattleDefeatPanel battleDefeatPanel;
    DungeonClearPanel dungeonClearPanel;
    MonsterStatePanel monsterStatePanel;
    BattleTextPanel battleTextPanel;

    private void Awake()
    {
        battleManager = GetComponentInParent<BattleManager>();

        characterStatePanel = GetComponentInChildren<CharacterStatePanel>();
        actionButtonPanel = GetComponentInChildren<ActionButtonPanel>();
        roulettePanel = GetComponentInChildren<RoulettePanel>();
        nextStagePanel = GetComponentInChildren<NextStagePanel>();
        battleDefeatPanel = GetComponentInChildren<BattleDefeatPanel>();
        dungeonClearPanel = GetComponentInChildren<DungeonClearPanel>();
        monsterStatePanel = GetComponentInChildren<MonsterStatePanel>();
        battleTextPanel = GetComponentInChildren<BattleTextPanel>();

        PanelInit();
    }

    private void PanelInit()
    {
        characterStatePanel.Init();
        actionButtonPanel.Init(battleManager);
        roulettePanel.Init();
        nextStagePanel.Init(battleManager);
        battleDefeatPanel.Init();
        dungeonClearPanel.Init();
        monsterStatePanel.Init();
        battleTextPanel.Init();

        nextStagePanel.gameObject.SetActive(false);
        battleDefeatPanel.gameObject.SetActive(false);
        dungeonClearPanel.gameObject.SetActive(false);
        monsterStatePanel.gameObject.SetActive(false);
    }

    public void UpdateActionBar()
    {
        characterStatePanel.UpdateActionBar(character);
    }

    public void SetRoulette(int i)
    {
        roulettePanel.SetRoulette(battleManager, i);
    }

    public void ClearRoulette()
    {
        roulettePanel.ClearRoulette();
    }

    public void UpdateCharacterState()
    {
        characterStatePanel.UpdateCharacterState(character);
    }

    public void UpdateMonsterState()
    {
        monsterStatePanel.UpdateMonsterState(selectMonster);
    }

    public void UpdateBattleState(string text)
    {
        battleTextPanel.UpdateBattleState(text);
    }

    public void RouletteButtonOn()
    {
        actionButtonPanel.rouletteButton.gameObject.SetActive(true);
    }
    public void NextStagePanelOn()
    {
        nextStagePanel.gameObject.SetActive(true);
    }
    public void BattleDefeatPanelOn()
    {
        battleDefeatPanel.gameObject.SetActive(true);
    }
    public void DungeonClearPanelOn()
    {
        dungeonClearPanel.gameObject.SetActive(true);
    }
    public void MonsterStatePanelOn()
    {
        monsterStatePanel.gameObject.SetActive(true);
    }
    
    public void RouletteButtonOff()
    {
        actionButtonPanel.rouletteButton.gameObject.SetActive(false);
    }
    public void NextStagePanelOff()
    {
        nextStagePanel.gameObject.SetActive(false);
    }
}
