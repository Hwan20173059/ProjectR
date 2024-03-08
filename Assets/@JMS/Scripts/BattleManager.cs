using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;

public class BattleManager : MonoBehaviour
{
    Player player;
    List<Slime> monsters = new List<Slime>();

    [SerializeField] private Transform PlayerArea;
    [SerializeField] private Transform EnemyArea;

    [SerializeField] private DungeonManager dungeonManager;

    public PlayerInput Input { get; private set; }
    public BattleUIController UIController { get; private set; }

    public bool selectingMonster;

    WaitForSeconds WaitFor1sec = new WaitForSeconds(1f);


    private void Awake()
    {
        Input = GetComponent<PlayerInput>();
        UIController = GetComponentInChildren<BattleUIController>();
    }
    private void Start()
    {
        Input.ClickActions.MouseClick.canceled += OnClickCancel;
    }

    private void OnClickCancel(InputAction.CallbackContext context)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Input.ClickActions.MousePos.ReadValue<Vector2>());
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (selectingMonster && hit.collider != null && hit.collider.CompareTag("Monster"))
        {
            StartCoroutine(PlayerAttack(hit.collider.transform.GetSiblingIndex()));
        }
    }

    public void FieldInit()
    {
        if(player == null)
            player = PlayerArea.GetComponentInChildren<Player>();

        for (int i = 0; i < EnemyArea.childCount; i++)
        {
            monsters.Add(EnemyArea.GetChild(i).GetComponent<Slime>());
        }
    }

    IEnumerator PlayerAttack(int index)
    {
        selectingMonster = false;
        UIController.AttackCancelPanel.SetActive(false);
        UIController.VoidPanel.SetActive(true);

        int beforeHp = monsters[index].hp;
        monsters[index].hp -= player.attack;

        UIController.BattleText.text = $"{player.name}의 공격!\n{monsters[index].name}이 {beforeHp - monsters[index].hp}의 데미지를 받았다!";
        Debug.Log($"{player.name}의 공격!\n{monsters[index].name}이 {beforeHp - monsters[index].hp}의 데미지를 받았다!");

        yield return WaitFor1sec;

        if (monsters[index].hp <= 0)
        {
            monsters[index].hp = 0;
            StartCoroutine(MonsterReward(index));
        }
        else
        {
            StartCoroutine(MonsterAttack());
        }
    }

    IEnumerator MonsterReward(int index)
    {
        player.CurrentExp += monsters[index].exp;
        UIController.BattleText.text = $"경험치 {monsters[index].exp} 획득!";
        Debug.Log($"경험치 {monsters[index].exp} 획득!");
        monsters.RemoveAt(index);
        Destroy(EnemyArea.GetChild(index).gameObject);

        yield return WaitFor1sec;

        if (monsters.Count == 0)
        {
            UIController.BattleText.text = $"모든 적을 처치했다!\n다음 스테이지로.";
            Debug.Log($"모든 적을 처치했다!\n다음 스테이지로.");

            yield return WaitFor1sec;

            UIController.VoidPanel.SetActive(false);
            dungeonManager.NextStage();
        }
        else
            StartCoroutine(MonsterAttack());
    }

    IEnumerator MonsterAttack()
    {
        for (int i = 0; i < monsters.Count; i++)
        {
            int beforeHp = player.currentHp;
            player.currentHp -= monsters[i].attack;

            UIController.BattleText.text = $"{monsters[i].name}의 공격!\n{player.name}이 {beforeHp - player.currentHp}의 데미지를 받았다!";
            Debug.Log($"{monsters[i].name}의 공격!\n{player.name}이 {beforeHp - player.currentHp}의 데미지를 받았다!");

            yield return WaitFor1sec;

            if (player.currentHp <= 0)
            {
                player.currentHp = 0;
                UIController.BattleText.text = $"{player.name}이 죽었습니다.\n게임 오버";
                Debug.Log($"{player.name}이 죽었습니다.\n게임 오버");

                yield return WaitFor1sec;

                dungeonManager.BattleEnd();
            }
        }

        UIController.BattleText.text = $"{player.name}의 차례!";
        Debug.Log($"{player.name}의 차례!");
        UIController.VoidPanel.SetActive(false);
    }
}