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

    InfiniteInventory infiniteInventory;
    Settings settings;

    ObjectPool objectPool;

    public GameObject characterHpBarPrefab;
    public GameObject monsterHpBarPrefab;

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

        infiniteInventory = GetComponentInChildren<InfiniteInventory>();
        settings = GetComponentInChildren<Settings>();

        objectPool = GetComponent<ObjectPool>();
        objectPool.Init();
    }

    private void Start()
    {
        PanelInit();
    }

    void PanelInit()
    {
        characterStatePanel.Init();
        actionButtonPanel.Init(battleManager);
        roulettePanel.Init(battleManager);
        nextStagePanel.Init(battleManager);
        battleDefeatPanel.Init();
        dungeonClearPanel.Init();
        monsterStatePanel.Init();
        battleTextPanel.Init();
        autoBattlePanel.Init(battleManager);
        menuButton.button.onClick.AddListener(MenuPanelOn);
        menuPanel.Init(this);

        settings.gameObject.SetActive(false);
    }

    public void CreateCharacterHpBar(Character character)
    {
        GameObject go = Instantiate(characterHpBarPrefab, gameObject.transform.GetChild(0).transform);
        CharacterHpBar hpBar = go.GetComponent<CharacterHpBar>();
        hpBar.Init(character);
        character.hpBar = hpBar;
    }

    public void CreateMonsterHpBar(Monster monster)
    {
        GameObject go = Instantiate(monsterHpBarPrefab, gameObject.transform.GetChild(0).transform);
        MonsterHpBar hpBar = go.GetComponent<MonsterHpBar>();
        hpBar.Init(monster);
        monster.hpBar = hpBar;
    }

    public void SetChangeHpTMP(int value, Vector3 screenPos)
    {
        GameObject go = objectPool.GetFromPool("ChangeHpTMP");
        int addPos = Random.Range(-100, 101);
        Vector3 randomPos = new Vector3(screenPos.x + addPos, screenPos.y + 100);
        go.transform.position = randomPos;
        go.GetComponent<ChangeHpTMP>().SetChangeHpTMP(value);
    }

    public void SetDurationEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos;
        go.GetComponent<BattleEffect>().SetDurationEffect(id);
    }

    public void SetRepeatEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos;
        go.GetComponent<BattleEffect>().SetRepeatEffect(id);
    }

    public void SetMoveEffect(int id, Vector3 startPos)
    {
        GameObject go = objectPool.GetFromPool("BattleEffect");
        go.transform.position = startPos;
        go.GetComponent<BattleEffect>().SetMoveEffect(id);
    }

    public void BattleEffectOff()
    {
        objectPool.SetActiveFalseAll("BattleEffect");
    }

    public void SetRoulette(int resultIndex0, int resultIndex1, int resultIndex2)
    {
        roulettePanel.SetRoulette(resultIndex0, resultIndex1, resultIndex2);
    }

    public void UpdateActionBar()
    {
        characterStatePanel.UpdateActionBar(character);
    }


    public void UpdateCharacterState()
    {
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

    public void OnClickItemUseButton()
    {
        infiniteInventory.consumeInventoryUI.SetActive(true);
        infiniteInventory.detailArea.gameObject.SetActive(true);
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
}
