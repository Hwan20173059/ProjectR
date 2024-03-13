using System;
using System.Collections;
using System.Collections.Generic;
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
    public GameObject battleDefeatPanel;
    public Button returnTownButton;
    public GameObject nextStagePanel;
    public Button nextStageButton;
    public GameObject dungeonClearPanel;
    public Button dungeonClearButton;

    private void Awake()
    {
        battleManager = GetComponentInParent<BattleManager>();

        attackButton.onClick.AddListener(OnClickAttackButton);
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
        if (character.IsSelectingAction && selectMonster != null &&!(selectMonster.IsDead))
        {
            character.selectAction = CharacterAction.Attack;
            battleManager.performList.Add(100);
            character.stateMachine.ChangeState(character.stateMachine.readyState);
        }
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
        SceneManager.LoadScene("BattleEndTestScene");
    }
}
