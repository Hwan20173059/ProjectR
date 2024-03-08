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

        UIController.BattleText.text = $"{player.name}�� ����!\n{monsters[index].name}�� {beforeHp - monsters[index].hp}�� �������� �޾Ҵ�!";
        Debug.Log($"{player.name}�� ����!\n{monsters[index].name}�� {beforeHp - monsters[index].hp}�� �������� �޾Ҵ�!");

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
        UIController.BattleText.text = $"����ġ {monsters[index].exp} ȹ��!";
        Debug.Log($"����ġ {monsters[index].exp} ȹ��!");
        monsters.RemoveAt(index);
        Destroy(EnemyArea.GetChild(index).gameObject);

        yield return WaitFor1sec;

        if (monsters.Count == 0)
        {
            UIController.BattleText.text = $"��� ���� óġ�ߴ�!\n���� ����������.";
            Debug.Log($"��� ���� óġ�ߴ�!\n���� ����������.");

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

            UIController.BattleText.text = $"{monsters[i].name}�� ����!\n{player.name}�� {beforeHp - player.currentHp}�� �������� �޾Ҵ�!";
            Debug.Log($"{monsters[i].name}�� ����!\n{player.name}�� {beforeHp - player.currentHp}�� �������� �޾Ҵ�!");

            yield return WaitFor1sec;

            if (player.currentHp <= 0)
            {
                player.currentHp = 0;
                UIController.BattleText.text = $"{player.name}�� �׾����ϴ�.\n���� ����";
                Debug.Log($"{player.name}�� �׾����ϴ�.\n���� ����");

                yield return WaitFor1sec;

                dungeonManager.BattleEnd();
            }
        }

        UIController.BattleText.text = $"{player.name}�� ����!";
        Debug.Log($"{player.name}�� ����!");
        UIController.VoidPanel.SetActive(false);
    }
}