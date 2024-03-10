using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterSO BaseData;

    public int level = 1;
    public int maxHP;
    public int curHP;
    public int atk;
    public int exp;
    public float curCoolTime;
    public float maxCoolTime;
    public bool IsDead => curHP <= 0;

    public Animator Animator { get; private set; }

    public MonsterStateMachine stateMachine;

    private BattleManager battleManager;

    private void Awake()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        stateMachine = new MonsterStateMachine(this);

        Animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        stateMachine.ChangeState(stateMachine.WaitState);

        Init(level);
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
        maxHP = BaseData.hp * level;
        curHP = BaseData.hp * level;
        atk = BaseData.atk * level;
        exp = BaseData.exp * level;
        maxCoolTime = BaseData.actionCoolTime;
    }
}
