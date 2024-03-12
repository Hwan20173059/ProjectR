using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Data")]
    public CharacterSO BaseData;

    [Header("Info")]
    public Sprite sprite;
    public string name;
    public string desc;

    [Header("Level")]
    public int level = 1;
    public int curExp;
    public int needExp;

    [Header("Stat")]
    public int maxHP;
    public int curHP;
    public int atk;

    public float curCoolTime;
    public float maxCoolTime;

    [Header("State")]
    public bool isSelected;
    public bool isBuy;

    [Header("System")]
    public Image ActionBar;

    public CharacterStateMachine stateMachine;
    public BattleManager BattleManager;

    public bool IsDead => curHP <= 0;
    public Animator Animator { get; private set; }

    private void Awake()
    {
        BattleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();

        stateMachine = new CharacterStateMachine(this);

        Animator = GetComponentInChildren<Animator>();
        ActionBar = BattleManager.ActionBar;

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
        needExp = BaseData.needExp * level;
        maxCoolTime = BaseData.actionCoolTime;
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if(curHP < 0 ) { curHP = 0; }
    }
}
