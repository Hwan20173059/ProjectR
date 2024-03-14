using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Data")]
    public CharacterSO baseData;

    [Header("Info")]
    public Sprite sprite;
    public string name;
    public string desc;

    [Header("Level")]
    public int level;
    public int curExp;
    public int needExp;

    [Header("Stat")]
    public int maxHP;
    public int curHP;
    public int atk;
    public bool IsDead => curHP <= 0;

    public List<CharacterAction> actions;
    public CharacterAction selectAction;

    public float curCoolTime;
    public float maxCoolTime;

    public Animator animator {  get; private set; }
    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;

    [Header("System")]
    public CharacterHpBar hpBar;
    public CharacterStateMachine stateMachine;
    public BattleManager battleManager;

    private void Awake()
    {
        hpBar = GetComponentInChildren<CharacterHpBar>();

        stateMachine = new CharacterStateMachine(this);

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
        maxHP = baseData.hp * level;
        curHP = baseData.hp * level;
        atk = baseData.atk * level;
        needExp = baseData.needExp * level;
        actions = baseData.actions;
        maxCoolTime = baseData.actionCoolTime;

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
    }

    public void ChangeExp(int change)
    {
        curExp += change;
        curExp = curExp >= needExp ? LevelUp() : curExp;
    }

    public int LevelUp()
    {
        curExp = curExp - needExp;
        curExp = curExp >= needExp ? LevelUp() : curExp;
        level++;
        level = level > baseData.maxLevel ? baseData.maxLevel : level;
        return curExp;
    }
}
