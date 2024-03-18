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

    public Image actionBar;
    public Button attackButton;
    public Button rouletteButton;
    public GameObject battleDefeatPanel;
    public Button returnTownButton;
    public GameObject nextStagePanel;
    public Button nextStageButton;
    public GameObject dungeonClearPanel;
    public Button dungeonClearButton;
    public GameObject monsterInfoPanel;
    public TMP_Text characterStateText;
    public TMP_Text monsterStateText;

    public Image roulette3;
    public Image roulette2;
    public Image roulette1;
    public Sprite voidRoulette;

    private void Awake()
    {
        battleManager = GetComponentInParent<BattleManager>();

        attackButton.onClick.AddListener(OnClickAttackButton);
        rouletteButton.onClick.AddListener(OnClickRouletteButton);
        returnTownButton.onClick.AddListener(TownSceneLoad);
        nextStageButton.onClick.AddListener(NextStageStart);
        dungeonClearButton.onClick.AddListener(TownSceneLoad);
    }

    private void Update()
    {
        if (character != null)
            actionBar.transform.localScale =
                new Vector3(Mathf.Clamp(character.curCoolTime / character.maxCoolTime, 0, character.maxCoolTime), 1, 1);
    }

    void OnClickAttackButton()
    {
        if (battleManager.IsSelectingAction && selectMonster != null &&!(selectMonster.IsDead))
        {
            character.selectAction = CharacterAction.Attack;
            character.curCoolTime = 0f;
            battleManager.performList.Add(100);
            character.stateMachine.ChangeState(character.stateMachine.readyState);
        }
    }

    void OnClickRouletteButton()
    {
        if (!battleManager.IsRouletteUsed)
        {
            battleManager.StartCoroutine(battleManager.Roulette());
            battleManager.IsRouletteUsed = true;

            rouletteButton.gameObject.SetActive(false);
        } 
    }

    public void SetRoulette(int i)
    {
        switch (i)
        {
            case 0: roulette1.sprite = battleManager.rouletteResult[i].equipSprite; break;
            case 1: roulette2.sprite = battleManager.rouletteResult[i].equipSprite; break;
            case 2: roulette3.sprite = battleManager.rouletteResult[i].equipSprite; break;
        }
    }

    public void ClearRoulette()
    {
        roulette1.sprite = voidRoulette;
        roulette2.sprite = voidRoulette;
        roulette3.sprite = voidRoulette;
    }

    void NextStageStart()
    {
        nextStagePanel.SetActive(false);
        character.curCoolTime = 0;
        Destroy(battleManager.monsterPool);
        battleManager.monsterPool = null;
        battleManager.monsters.Clear();
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.startState);
    }

    void TownSceneLoad()
    {
        battleDefeatPanel.SetActive(false);
        SceneManager.LoadScene("TownScene");
    }

    public void CharacterStateUpdate()
    {
        if (character == null) return;
        characterStateText.text = $"캐릭터 : {character.characterName}\n레벨 : {character.level}\n" +
            $"체력 : {character.curHP} / {character.maxHP}\n상태 : {character.currentState}";
    }

    public void MonsterStateUpdate()
    {
        if (selectMonster == null) return;
        monsterStateText.text = $"{selectMonster.monsterName} {selectMonster.curHP} / {selectMonster.maxHP}";
    }
}
