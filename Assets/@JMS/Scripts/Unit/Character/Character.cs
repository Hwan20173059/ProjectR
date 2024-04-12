using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System.Collections;
using System.Collections.Generic;
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
    public int maxLevel => 30 + (baseData.grade * 5);

    [Header("Stat")]
    public int maxHP;
    public int curHP;
    public int atk;

    public float curCoolTime;
    public float maxCoolTime;

    public int changedAtk { get { return atk + characterBuffHandler.atkBuff; } }
    public float changedMaxCoolTime { get { return maxCoolTime - characterBuffHandler.speedBuff; } }

    public bool IsDead => curHP <= 0;
    public string currentStateText = "´ë±âÁß";

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

    public void LoadCharacter(Character character)
    {
        baseData = character.baseData;
        characterName = baseData.characterName;
        maxCoolTime = baseData.actionCoolTime;
        level = character.level;
        maxHP = character.maxHP;
        curHP = character.curHP;
        atk = character.atk;
        needExp = character.needExp;
        curExp = character.curExp;

        spriteRenderer.sprite = Resources.Load<Sprite>(baseData.spritePath);
        animator.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(baseData.animatorPath);
    }

    public void SaveCharacter(Character saveCharacter)
    {
        saveCharacter.level = level;
        saveCharacter.curExp = curExp;
        saveCharacter.needExp = needExp;
        saveCharacter.maxHP = maxHP;
        saveCharacter.curHP = curHP;
        saveCharacter.atk = atk;
    }

    public void LoadInit(CharacterData characterData, int level, int currentExp)
    {
        baseData = characterData;
        this.level = level;
        maxHP = baseData.hp + (baseData.levelUpHp * level);
        curHP = maxHP;
        atk = baseData.atk + (baseData.levelUpAtk * level);
        needExp = 100 + (level * (10 * ((level + 5) / 5)));
        curExp = currentExp;

        sprite = Resources.Load<Sprite>(baseData.spritePath);
        spriteRenderer.sprite = sprite;
    }

    public void CoolTimeUpdate()
    {
        if (curCoolTime < changedMaxCoolTime)
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

    public void ChangeHP(int value)
    {
        curHP += value;
        curHP = curHP > maxHP ? maxHP : curHP;
        curHP = curHP < 0 ? 0 : curHP;

        hpBar.SetHpBar();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        battleCanvas.SetChangeHpTMP(value, screenPos);

        if (curHP <= 0)
        {
            stateMachine.ChangeState(stateMachine.deadState);
        }

        battleCanvas.UpdateCharacterState();
    }

    public void ChangeExp(int value)
    {
        curExp += value;
        curExp = curExp >= needExp ? LevelUp() : curExp;
    }

    public int LevelUp()
    {
        curExp = curExp - needExp;
        level++;
        level = level > maxLevel ? maxLevel : level;
        maxHP = baseData.hp + ( baseData.levelUpHp * level );
        curHP += baseData.levelUpHp;
        atk = baseData.atk + ( baseData.levelUpAtk * level );
        needExp = 100 + (level * (10 * ((level + 5) / 5)));
        curExp = curExp >= needExp ? LevelUp() : curExp;
        return curExp;
    }

    public void ReduceBuffDuration()
    {
        characterBuffHandler.ReduceBuffDuration();
    }
}
