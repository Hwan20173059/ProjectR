using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;
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
    BattleVictoryPanel battleVictoryPanel;
    MonsterStatePanel monsterStatePanel;
    BattleTextPanel battleTextPanel;
    AutoBattlePanel autoBattlePanel;
    MenuButton menuButton;
    MenuPanel menuPanel;
    CheatPanel cheatPanel;
    StagePanel stagePanel;
    BuffDescriptionPanel buffDescriptionPanel;
    UseItemPanel useItemPanel;
    LoadingUI loadingUI;

    Settings settings;

    ObjectPool objectPool;

    [SerializeField] private Transform unitHpBar;
    [SerializeField] private Transform characterBuff;
    [SerializeField] private Transform useItemSlots;

    UseItemSlot selectUseItemSlot;

    public void Init()
    {
        battleManager = GetComponentInParent<BattleManager>();

        objectPool = GetComponent<ObjectPool>();
        characterStatePanel = GetComponentInChildren<CharacterStatePanel>();
        actionButtonPanel = GetComponentInChildren<ActionButtonPanel>();
        roulettePanel = GetComponentInChildren<RoulettePanel>();
        nextStagePanel = GetComponentInChildren<NextStagePanel>();
        battleDefeatPanel = GetComponentInChildren<BattleDefeatPanel>();
        battleVictoryPanel = GetComponentInChildren<BattleVictoryPanel>();
        monsterStatePanel = GetComponentInChildren<MonsterStatePanel>();
        battleTextPanel = GetComponentInChildren<BattleTextPanel>();
        autoBattlePanel = GetComponentInChildren<AutoBattlePanel>();
        menuButton = GetComponentInChildren<MenuButton>();
        menuPanel = GetComponentInChildren<MenuPanel>();
        cheatPanel = GetComponentInChildren<CheatPanel>();
        stagePanel = GetComponentInChildren<StagePanel>();
        buffDescriptionPanel = GetComponentInChildren<BuffDescriptionPanel>();
        useItemPanel = GetComponentInChildren<UseItemPanel>();

        objectPool.Init();
        characterStatePanel.Init();
        actionButtonPanel.Init(battleManager);
        roulettePanel.Init(battleManager);
        nextStagePanel.Init(battleManager);
        battleDefeatPanel.Init(this);
        battleVictoryPanel.Init(this);
        monsterStatePanel.Init();
        battleTextPanel.Init();
        autoBattlePanel.Init(battleManager, false);
        menuButton.Init(this);
        menuPanel.Init(this);
        cheatPanel.Init(battleManager);
        stagePanel.Init();
        buffDescriptionPanel.Init();
        useItemPanel.Init(this);

        settings = GetComponentInChildren<Settings>();
        settings.gameObject.SetActive(false);

        loadingUI = GetComponentInChildren<LoadingUI>();
        loadingUI.OpenScreen();
    }

    public void SetCharacterHpBar(Character character)
    {
        GameObject go = objectPool.GetFromPool("CharacterHpBar");
        go.gameObject.transform.SetParent(unitHpBar);
        CharacterHpBar hpBar = go.GetComponent<CharacterHpBar>();
        hpBar.Init(character);
        character.hpBar = hpBar;
    }

    public void SetMonsterHpBar(Monster monster)
    {
        GameObject go = objectPool.GetFromPool("MonsterHpBar");
        go.gameObject.transform.SetParent(unitHpBar);
        MonsterHpBar hpBar = go.GetComponent<MonsterHpBar>();
        hpBar.Init(monster);
        monster.hpBar = hpBar;
    }

    public void SetChangeHpTMP(int value, Vector3 screenPos)
    {
        GameObject go = objectPool.GetFromPool("ChangeHpTMP");
        int addPos = Random.Range(-50, 51);
        Vector3 randomPos = new Vector3(screenPos.x + addPos, screenPos.y + 70);
        go.transform.position = randomPos;
        go.GetComponent<ChangeHpTMP>().SetChangeHpTMP(value);
    }

    public BuffIcon SetBuff(Buff buff)
    {
        GameObject go = objectPool.GetFromPool("BuffIcon");
        go.transform.SetParent(characterBuff);
        BuffIcon buffIcon = go.GetComponent<BuffIcon>();
        buffIcon.Init(this, buff);
        return buffIcon;
    }

    public void SetUseItemSlot(ConsumeItem consumeItem)
    {
        GameObject go = objectPool.GetFromPool("UseItemSlot");
        go.transform.SetParent(useItemSlots);
        UseItemSlot useItemSlot = go.GetComponent<UseItemSlot>();
        useItemSlot.Init(this, consumeItem);
    }

    public void UpdateBuffText(Buff buff)
    {
        buffDescriptionPanel.gameObject.SetActive(true);
        buffDescriptionPanel.UpdateBuffText(buff);
    }

    public void SetRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        roulettePanel.SetRoulette(resultIndex0, resultIndex1, resultIndex2);
    }

    public void UpdateActionBar()
    {
        characterStatePanel.UpdateActionBar(character);
    }

    public void UpdateCharacterState(bool IsRouletteUsed)
    {
        if (IsRouletteUsed)
            characterStatePanel.UpdateCharacterState(character, battleManager);
        else
            characterStatePanel.UpdateCharacterState(character);
    }

    public void UpdateMonsterState()
    {
        monsterStatePanel.UpdateMonsterState(selectMonster);
    }

    public void UpdateBattleText(string text)
    {
        battleTextPanel.UpdateBattleText(text);
    }

    public void RouletteButtonOn()
    {
        actionButtonPanel.RouletteButtonOn();
    }
    public void NextStagePanelOn()
    {
        nextStagePanel.gameObject.SetActive(true);
    }
    public void BattleDefeatPanelOn()
    {
        battleDefeatPanel.gameObject.SetActive(true);

        Time.timeScale = 1f;
    }
    public void DungeonClearPanelOn()
    {
        battleVictoryPanel.gameObject.SetActive(true);
    }
    public void MonsterStatePanelOn()
    {
        monsterStatePanel.gameObject.SetActive(true);
    }
    public void MonsterStatePanelOff()
    {
        monsterStatePanel.gameObject.SetActive(false);
    }
    public void MenuPanelOn()
    {
        menuPanel.gameObject.SetActive(true);
    }

    public void SettingsOn()
    {
        settings.gameObject.SetActive(true);
    }

    public void RouletteButtonOff()
    {
        actionButtonPanel.RouletteButtonOff();
    }
    public void NextStagePanelOff()
    {
        nextStagePanel.gameObject.SetActive(false);
    }
    public void BuffDescriptionPanelOff()
    {
        buffDescriptionPanel.gameObject.SetActive(false);
    }

    public void CloseScreen(string loadScene)
    {
        loadingUI.gameObject.SetActive(true);
        loadingUI.CloseScreen(loadScene);
    }

    public void UpdateStageText(int curStage, int stageCount)
    {
        stagePanel.UpdateStageText(curStage, stageCount);
    }

    public void SelectUseItem(UseItemSlot selectUseItemSlot)
    {
        if (this.selectUseItemSlot != null)
            this.selectUseItemSlot.SlotColorClear();

        this.selectUseItemSlot = selectUseItemSlot;
    }

    public void UseItemPanelOn()
    {
        useItemPanel.gameObject.SetActive(true);
    }

    public void OnClickItemUseButton()
    {
        if (selectUseItemSlot == null) { return; }

        battleManager.UseItem(selectUseItemSlot.consumeItem);
    }

    public void UpdateUseItemSlot()
    {
        if (selectUseItemSlot.consumeItem.count <= 0)
        {
            selectUseItemSlot.gameObject.SetActive(false);
            selectUseItemSlot = null;
        }
        else
            selectUseItemSlot.UpdateUseItemSlot();

    }

}
