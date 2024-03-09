using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJMS : MonoBehaviour
{
    public CharacterSO BaseData;
    public int level = 1;
    public int maxHP;
    public int curHP;
    public int atk;
    public int curExp;
    public int needExp;

    public Animator Animator {  get; private set; }

    public CharacterStateMachine stateMachine;

    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();

        stateMachine = new CharacterStateMachine(this);
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
        this.level = level;
        maxHP = BaseData.hp * level;
        curHP = BaseData.hp * level;
        atk = BaseData.atk * level;
        needExp = BaseData.needExp * level;
    }
}
