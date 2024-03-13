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

    public Image ActionBar;
    public Button AttackButton;
    public GameObject BattleDefeatPanel;
    public Button ReturnTownButton;
    public GameObject NextStagePanel;
    public Button NextStageButton;
    public GameObject DungeonClearPanel;
    public Button DungeonClearButton;

    private void Awake()
    {
        battleManager = GetComponentInParent<BattleManager>();
        
        AttackButton.onClick.AddListener(OnClickAttackButton);
        ReturnTownButton.onClick.AddListener(TownSceneLoad);
        NextStageButton.onClick.AddListener(NextStageStart);
        DungeonClearButton.onClick.AddListener(TownSceneLoad);
    }

    private void Update()
    {
        if (battleManager.Character != null)
            ActionBar.transform.localScale =
                new Vector3(Mathf.Clamp(battleManager.Character.curCoolTime / battleManager.Character.maxCoolTime, 0, battleManager.Character.maxCoolTime), 1, 1);
    }

    void OnClickAttackButton()
    {
        if (battleManager.Character.stateMachine.currentState is CharacterSelectActionState && battleManager.selectMonster != null
            &&!(battleManager.selectMonster.stateMachine.currentState is MonsterDeadState))
        {
            battleManager.Character.selectAction = CharacterAction.Attack;
            battleManager.PerformList.Add(100);
            battleManager.Character.stateMachine.ChangeState(battleManager.Character.stateMachine.ReadyState);
        }
    }

    void NextStageStart()
    {
        NextStagePanel.SetActive(false);
        battleManager.Character.curCoolTime = 0;
        Destroy(battleManager.monsterPool);
        battleManager.monsterPool = null;
        battleManager.Monsters.Clear();
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.StartState);
    }

    void TownSceneLoad()
    {
        BattleDefeatPanel.SetActive(false);
        SceneManager.LoadScene("BattleEndTestScene");
    }
}
