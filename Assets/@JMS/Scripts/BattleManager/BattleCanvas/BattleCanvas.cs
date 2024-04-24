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
    DungeonClearPanel dungeonClearPanel;
    MonsterStatePanel monsterStatePanel;
    BattleTextPanel battleTextPanel;
    AutoBattlePanel autoBattlePanel;
    MenuButton menuButton;
    MenuPanel menuPanel;
    CheatPanel cheatPanel;
    StagePanel stagePanel;
    BuffDescriptionPanel buffDescriptionPanel;
    LoadingUI loadingUI;

    InfiniteInventory infiniteInventory;
    Settings settings;

    ObjectPool objectPool;

    [SerializeField] private Transform unitHpBar;
    [SerializeField] private Transform characterBuff;

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
        autoBattlePanel = GetComponentInChildren<AutoBattlePanel>();
        menuButton = GetComponentInChildren<MenuButton>();
        menuPanel = GetComponentInChildren<MenuPanel>();
        cheatPanel = GetComponentInChildren<CheatPanel>();
        stagePanel = GetComponentInChildren<StagePanel>();
        buffDescriptionPanel = GetComponentInChildren<BuffDescriptionPanel>();
        loadingUI = GetComponentInChildren<LoadingUI>();

        infiniteInventory = GetComponentInChildren<InfiniteInventory>();
        settings = GetComponentInChildren<Settings>();

        objectPool = GetComponent<ObjectPool>();
        objectPool.Init();
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        characterStatePanel.Init();
        actionButtonPanel.Init(battleManager);
        roulettePanel.Init(battleManager);
        nextStagePanel.Init(battleManager);
        battleDefeatPanel.Init(this);
        dungeonClearPanel.Init(this);
        monsterStatePanel.Init();
        battleTextPanel.Init();
        autoBattlePanel.Init(battleManager);
        menuButton.button.onClick.AddListener(MenuPanelOn);
        menuPanel.Init(this);
        cheatPanel.Init(battleManager);
        stagePanel.Init(battleManager);
        buffDescriptionPanel.Init();

        cheatPanel.gameObject.SetActive(false);

        settings.gameObject.SetActive(false);

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

    public void SetBuffText(Buff buff)
    {
        buffDescriptionPanel.gameObject.SetActive(true);
        buffDescriptionPanel.SetBuffText(buff);
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
    public void MonsterStatePanelOff()
    {
        monsterStatePanel.gameObject.SetActive(false);
    }
    private void MenuPanelOn()
    {
        menuPanel.gameObject.SetActive(true);
    }

    public void SettingsOn()
    {
        settings.gameObject.SetActive(true);
    }

    public void RouletteButtonOff()
    {
        actionButtonPanel.rouletteButton.gameObject.SetActive(false);
    }
    public void NextStagePanelOff()
    {
        nextStagePanel.gameObject.SetActive(false);
    }
    public void BuffDescriptionPanelOff()
    {
        buffDescriptionPanel.gameObject.SetActive(false);
    }


    public void OnClickItemUseButton()
    {
        infiniteInventory.OpenConsumeInventory();
    }

    public void OnClickUseButton()
    {
        battleManager.UseItem(infiniteInventory.detailArea.nowConsumeItem);
    }
    public void FreshConsumeSlot()
    {
        infiniteInventory.detailArea.ChangeDetailActivation(false);
        infiniteInventory.FreshConsumeSlot();
    }

    public void CloseScreen(string loadScene)
    {
        loadingUI.gameObject.SetActive(true);
        loadingUI.CloseScreen(loadScene);
    }

    public void SetStageText()
    {
        stagePanel.SetStageText();
    }
}
