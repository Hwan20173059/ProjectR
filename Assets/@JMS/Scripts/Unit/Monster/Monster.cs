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
    public bool IsDead => curHP <= 0;

    public List<MonsterAction> actions;
    public MonsterAction selectAction;

    public float curCoolTime;
    public float maxCoolTime;

    public int monsterNumber;

    public Animator Animator { get; private set; }
    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;

    public MonsterStateMachine stateMachine;

    public BattleManager battleManager;

    private void Awake()
    {
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
        actions = BaseData.actions;
        maxCoolTime = BaseData.actionCoolTime;
    }

    public void ChangeHP(int change)
    {
        curHP += change;
        curHP = curHP > maxHP ? maxHP : curHP;
        curHP = curHP < 0 ? 0 : curHP;

        if (curHP <= 0)
        {
            stateMachine.ChangeState(stateMachine.DeadState);
        }
    }
}
