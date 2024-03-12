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
    public bool IsDead => curHP <= 0;

    public List<CharacterAction> actions;
    public CharacterAction selectAction;

    public float curCoolTime;
    public float maxCoolTime;

    public Animator Animator {  get; private set; }
    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;


    [Header("State")]
    public bool isSelected;
    public bool isBuy;

    [Header("System")]
    public CharacterStateMachine stateMachine;
    public BattleManager battleManager;

    private void Awake()
    {
        stateMachine = new CharacterStateMachine(this);

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
        needExp = BaseData.needExp * level;
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
        level = level > BaseData.maxLevel ? BaseData.maxLevel : level;
        return curExp;
    }
}
