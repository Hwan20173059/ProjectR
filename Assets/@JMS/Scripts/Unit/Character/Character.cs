using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [Header("Data")]
    public CharacterData baseData;

    [Header("Info")]
    public string characterName;
    public Sprite sprite;
    public SpriteRenderer spriteRenderer;

    [Header("Level")]
    public int level;
    public int curExp;
    public int needExp;

    [Header("Stat")]
    public int maxHP;
    public int curHP;
    public int atk { get { return atk + characterBuffHandler.atkBuff; } set { atk = value; } }
    public bool IsDead => curHP <= 0;
    public string currentStateText = "´ë±âÁß";

    public float curCoolTime;
    public float maxCoolTime { get { return maxCoolTime - characterBuffHandler.speedBuff; } set { maxCoolTime = value; } }

    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;


    public CharacterBuffHandler characterBuffHandler;
    public Animator animator {  get; private set; }

    public CharacterHpBar hpBar;

    public BattleManager battleManager;
    public BattleCanvas battleCanvas { get {  return battleManager.battleCanvas; } }

    public CharacterStateMachine stateMachine;

    private void Awake()
    {
        characterBuffHandler = new CharacterBuffHandler();

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

    public void CharacterLoad(Character character)
    {
        baseData = character.baseData;
        level = character.level;
        curExp = character.curExp;
        curHP = character.curHP;
    }

    public void LoadInit(CharacterData characterData, int level, int currentExp)
    {
        this.level = level;
        this.curExp = currentExp;
        baseData = characterData;
        
        sprite = Resources.Load<Sprite>(characterData.spritePath);
        spriteRenderer.sprite = sprite;

        Init();
    }

    public void Init()
    {
        characterName = baseData.characterName;
        maxHP = baseData.hp * level;
        curHP = maxHP;
        atk = baseData.atk * level;
        needExp = baseData.needExp * level;
        maxCoolTime = baseData.actionCoolTime;

        spriteRenderer.sprite = Resources.Load<Sprite>(baseData.spritePath);
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(baseData.animatorPath);
    }

    public void CoolTimeUpdate()
    {
        if (curCoolTime < maxCoolTime)
        {
            curCoolTime += Time.deltaTime;
            battleManager.battleCanvas.UpdateActionBar();
        }
        else
        {
            if (battleManager.IsAutoBattle)
                stateMachine.ChangeState(stateMachine.autoSelectState);
            else
                stateMachine.ChangeState(stateMachine.selectActionState);
        }
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

    public void BuffDurationReduce()
    {
        if (characterBuffHandler.atkDuration > 0) { characterBuffHandler.atkDuration--; }
        if (characterBuffHandler.speedDuration > 0) { characterBuffHandler.speedDuration--; }
    }
}
