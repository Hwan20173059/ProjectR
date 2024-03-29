using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Data")]
    public CharacterData baseData;

    [Header("Info")]
    public string characterName;

    [Header("Level")]
    public int level;
    public int curExp;
    public int needExp;

    [Header("Stat")]
    public int maxHP;
    public int curHP;
    public int atk;
    public bool IsDead => curHP <= 0;
    public string currentStateText = "대기중";

    public float curCoolTime;
    public float maxCoolTime;

    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;

    public SpriteRenderer spriteRenderer { get; private set; }
    public Animator animator {  get; private set; }

    public CharacterHpBar hpBar;

    public BattleManager battleManager;
    public BattleCanvas battleCanvas { get {  return battleManager.battleCanvas; } }

    public CharacterStateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new CharacterStateMachine(this);

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

    public Character(CharacterData character) // 생성자
    {
        characterName = character.characterName;
        maxHP = character.hp * level;
        atk = character.atk * level;
        needExp = character.needExp * level;
        maxCoolTime = character.actionCoolTime;

        spriteRenderer.sprite = Resources.Load<Sprite>(character.spritePath);
        animator = Resources.Load<Animator>(character.animatorPath);
    }

    public void CharacterLoad(Character character)
    {
        baseData = character.baseData;
        level = character.level;
        curExp = character.curExp;
        curHP = character.curHP;
}

    public void Init()
    {
        characterName = baseData.characterName;
        maxHP = baseData.hp * level;
        atk = baseData.atk * level;
        needExp = baseData.needExp * level;
        maxCoolTime = baseData.actionCoolTime;

        spriteRenderer.sprite = Resources.Load<Sprite>(baseData.spritePath);
        animator = Resources.Load<Animator>(baseData.animatorPath);
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

        battleCanvas.UpdateCharacterState();
    }

    public void ChangeExp(int change)
    {
        curExp += change;
        curExp = curExp >= needExp ? LevelUp() : curExp;
    }

    public int LevelUp()
    {
        curExp = curExp - needExp;
        level++;
        level = level > baseData.maxLevel ? baseData.maxLevel : level;
        maxHP = baseData.hp * level;
        atk = baseData.atk * level;
        needExp = baseData.needExp * level;
        curExp = curExp >= needExp ? LevelUp() : curExp;
        return curExp;
    }
}
