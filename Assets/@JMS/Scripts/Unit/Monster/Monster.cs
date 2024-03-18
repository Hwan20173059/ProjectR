using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterSO BaseData;

    public string monsterName;
    public int level;
    public int maxHP;
    public int curHP;
    public int atk;
    public int exp;
    public bool IsDead => curHP <= 0;

    public List<MonsterAction> actions;
    public MonsterAction selectAction;

    public float curCoolTime;
    public float maxCoolTime;

    public int monsterNumber;

    public Animator animator { get; private set; }
    public Vector3 startPosition;
    public Vector3 attackPosition = new Vector3(-5.5f, 1.5f, 0);
    public float moveAnimSpeed = 10f;

    public MonsterHpBar hpBar;

    public MonsterStateMachine stateMachine;

    public BattleManager battleManager;
    public BattleCanvas battleCanvas { get {  return battleManager.battleCanvas; } }

    private void Awake()
    {
        hpBar = GetComponentInChildren<MonsterHpBar>();

        stateMachine = new MonsterStateMachine(this);

        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.waitState);
    }

    private void Update()
    {
        stateMachine.HandleInput();

        stateMachine.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    public void Init(int level)
    {
        monsterName = BaseData.monsterName;
        maxHP = BaseData.hp * level;
        curHP = BaseData.hp * level;
        atk = BaseData.atk * level;
        exp = BaseData.exp * level;
        actions = BaseData.actions;
        maxCoolTime = BaseData.actionCoolTime;

        hpBar.Init();
    }

    public void ChangeHP(int change)
    {
        curHP += change;
        curHP = curHP > maxHP ? maxHP : curHP;
        curHP = curHP < 0 ? 0 : curHP;

        hpBar.SetHpBar();

        if (curHP <= 0)
        {
            stateMachine.ChangeState(stateMachine.deadState);
        }

        battleCanvas.MonsterStateUpdate();
    }
}
