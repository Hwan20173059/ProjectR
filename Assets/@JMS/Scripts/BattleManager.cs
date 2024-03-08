using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    Character character;
    List<Slime> monsters = new List<Slime>();

    [SerializeField] private Transform PlayerArea;
    [SerializeField] private Transform EnemyArea;

    [SerializeField] private DungeonManager dungeonManager;

    public PlayerInput Input { get; private set; }
    public BattleUIController UIController { get; private set; }


    public bool selectingMonster;

    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        UIController = GetComponentInChildren<BattleUIController>();
    }
    private void Start()
    {
        Input.ClickActions.MouseClick.started += OnClickStart;
        Input.ClickActions.MouseClick.canceled += OnClickCancel;
    }

    private void OnClickStart(InputAction.CallbackContext context)
    {
        Time.timeScale = 1.0f;
    }

    private void OnClickCancel(InputAction.CallbackContext context)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.ClickActions.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (selectingMonster && hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            CharacterAttack(hit.collider.transform.GetSiblingIndex());
        }
    }

    public void FieldInit()
    {
        character = PlayerArea.GetComponentInChildren<Character>();

        for (int i = 0; i < EnemyArea.childCount; i++)
        {
            monsters.Add(EnemyArea.GetChild(i).GetComponent<Slime>());
        }
    }

    void CharacterAttack(int index)
    {
        selectingMonster = false;
        UIController.AttackCancelPanel.SetActive(false);
        UIController.VoidPanel.SetActive(true);

        int beforeHp = monsters[index].hp;
        monsters[index].hp -= character.attack;

        UIController.BattleText.text = $"{character.name}의 공격!\n{monsters[index].name}이 {beforeHp - monsters[index].hp}의 데미지를 받았다!";
        StopGame();

        if (monsters[index].hp <= 0)
        {
            monsters[index].hp = 0;
            MonsterReward(index);
        }
        else
        {
            MonsterAttack();
        }
    }

    void MonsterReward(int index)
    {
        character.currentExp += monsters[index].exp;
        UIController.BattleText.text = $"경험치 {monsters[index].exp} 획득!";
        monsters.RemoveAt(index);
        Destroy(EnemyArea.GetChild(index));
        StopGame();

        if (monsters.Count == 0)
        {
            UIController.BattleText.text = $"모든 적을 처치했다!\n다음 스테이지로.";
            StopGame();
            dungeonManager.NextStage();
        }
        else
            MonsterAttack();
    }

    void MonsterAttack()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            int beforeHp = character.currentHp;
            character.currentHp -= monsters[i].attack;

            UIController.BattleText.text = $"{monsters[i].name}의 공격!\n{character.name}이 {beforeHp - character.currentHp}의 데미지를 받았다!";
            StopGame();

            if (character.currentHp <= 0)
            {
                character.currentHp = 0;
                UIController.BattleText.text = $"{character.name}이 죽었습니다.\n게임 오버";
                StopGame();
                dungeonManager.BattleEnd();
            }
        }

        UIController.BattleText.text = $"{character.name}의 차례!";
        UIController.VoidPanel.SetActive(false);
    }

    void StopGame()
    {
        Time.timeScale = 0f;
    }
}