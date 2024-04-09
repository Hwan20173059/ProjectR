using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData baseData;

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

    public SpriteRenderer spriteRenderer { get; private set; }
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
        stateMachine = new MonsterStateMachine(this);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

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

    public void SetMonsterData(MonsterData monsterData)
    {
        baseData = monsterData;
    }

    public void Init(int level)
    {
        monsterName = baseData.monsterName;
        maxHP = baseData.hp * level;
        curHP = maxHP;
        atk = baseData.atk * level;
        exp = baseData.exp * level;
        maxCoolTime = baseData.actionCoolTime;

        for(int i = 0; i < baseData.actions.Length; i++)
        {
            actions.Add((MonsterAction)baseData.actions[i]);
        }

        spriteRenderer.sprite = Resources.Load<Sprite>(baseData.spritePath);
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(baseData.animatorPath);
    }

    public void ChangeHP(int value)
    {
        curHP += value;
        curHP = curHP > maxHP ? maxHP : curHP;
        curHP = curHP < 0 ? 0 : curHP;

        hpBar.SetHpBar();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        battleCanvas.ChangeHpTMP(value, screenPos);

        if (curHP <= 0)
        {
            stateMachine.ChangeState(stateMachine.deadState);
        }

        battleCanvas.UpdateMonsterState();
    }
}
