using Assets.PixelFantasy.PixelTileEngine.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.U2D.Animation;
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

    public CharacterAnimController animatorController { get; private set; }
    public SpriteRenderer spriteRenderer;
    public Sprite sprite;
    public SpriteLibrary spriteLibrary;

    public CharacterBuffHandler characterBuffHandler;

    public CharacterStateMachine stateMachine;

    public CharacterHpBar hpBar;

    public BattleManager battleManager;
    public BattleCanvas battleCanvas { get {  return battleManager.battleCanvas; } }

    private void Awake()
    {
        animatorController = GetComponent<CharacterAnimController>();
        spriteLibrary = GetComponentInChildren<SpriteLibrary>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        characterBuffHandler = new CharacterBuffHandler();

        stateMachine = new CharacterStateMachine(this);
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

        spriteLibrary.spriteLibraryAsset = character.spriteLibrary.spriteLibraryAsset;
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

    public void Init(CharacterData characterData, int level, int currentExp)
    {
        baseData = characterData;
        characterName = baseData.characterName;
        maxCoolTime = baseData.actionCoolTime;
        this.level = level;
        maxHP = baseData.hp + (baseData.levelUpHp * level);
        curHP = maxHP;
        atk = baseData.atk + (baseData.levelUpAtk * level);
        needExp = 50 + (10 * (level * level));
        curExp = currentExp;

        sprite = Resources.Load<Sprite>(baseData.spritePath);
        spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>(baseData.assetPath);
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

    public void PlayAnim(CharacterAnim anim)
    {
        animatorController.PlayAnim(anim);
    }

    public void ChangeAnimState(CharacterAnimState animState)
    {
        animatorController.ChangeAnimState(animState);
    }
}
