using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public CharacterSO BaseData;

    public int level = 1;
    public int maxHP;
    public int curHP;
    public int atk;
    public int curExp;
    public int needExp;
    public bool IsDead => curHP <= 0;

    public float curCoolTime;
    public float maxCoolTime;

    public Animator Animator {  get; private set; }

    public CharacterStateMachine stateMachine;

    public BattleManager battleManager;
    public Image ActionBar;

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
        maxCoolTime = BaseData.actionCoolTime;
    }

    public void TakeDamage(int damage)
    {
        curHP -= damage;
        if(curHP < 0 ) { curHP = 0; }
    }
}
