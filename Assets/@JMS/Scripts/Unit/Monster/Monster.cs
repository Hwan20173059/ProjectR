using Assets.PixelFantasy.PixelMonsters.Common.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using UnityEngine;
using UnityEngine.U2D.Animation;
using static UnityEngine.GraphicsBuffer;

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

    public bool IsFrozen;

    public bool IsStun;
    public float curStunTime;
    public float maxStunTime;
    BattleEffect stunEffect;

    public bool IsBurn;
    public float curBurnTime;
    public int burnDamage;
    public float burnInterval;
    public int burnCount;

    public MonsterAnimController monsterAnimController { get; private set; }
    public SpriteRenderer spriteRenderer;
    public SpriteLibrary spriteLibrary;

    public Vector3 startPosition;
    public float moveAnimSpeed = 10f;

    public MonsterHpBar hpBar;

    public MonsterStateMachine stateMachine;

    public BattleManager battleManager;
    public BattleCanvas battleCanvas { get { return battleManager.battleCanvas; } }

    private void Awake()
    {
        stateMachine = new MonsterStateMachine(this);

        monsterAnimController = GetComponent<MonsterAnimController>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        spriteLibrary = GetComponentInChildren<SpriteLibrary>();
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
        for (int i = 0; i < baseData.actions.Length; i++)
        {
            actions.Add((MonsterAction)baseData.actions[i]);
        }
        maxCoolTime = baseData.actionCoolTime;

        curCoolTime = 0f;

        IsBurn = false;
        burnDamage = 0;

        IsStun = false;
        curStunTime = 0f;
        maxStunTime = 0f;

        IsFrozen = false;

        RefreshAnim();

        spriteRenderer.sprite = Resources.Load<Sprite>(baseData.spritePath);
        spriteLibrary.spriteLibraryAsset = Resources.Load<SpriteLibraryAsset>(baseData.assetPath);
    }

    public void CoolTimeUpdate()
    {
        if (curCoolTime < maxCoolTime)
        {
            curCoolTime += Time.deltaTime * (100 / 100f);
        }
        else
        {
            stateMachine.ChangeState(stateMachine.selectActionState);
        }
    }

    public void ChangeHP(int value)
    {
        if (value < 0)
        {
            if (IsFrozen)
            {
                IsFrozen = false;
                RefreshAnim();
            }
            monsterAnimController.PlayAnim(MonsterAnim.Hit);
        }
        else if (value > 0)
        {
            monsterAnimController.PlayAnim(MonsterAnim.Heal);
        }

        curHP += value;
        curHP = curHP > maxHP ? maxHP : curHP;
        curHP = curHP < 0 ? 0 : curHP;

        hpBar.UpdateHpBar();

        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        battleCanvas.SetChangeHpTMP(value, screenPos);

        if (curHP <= 0)
        {
            if (IsStun) stunEffect.SetActive(false);
            stateMachine.ChangeState(stateMachine.deadState);
        }

        battleCanvas.UpdateMonsterState();
    }

    public void PlayAnim(MonsterAnim anim)
    {
        monsterAnimController.PlayAnim(anim);
    }

    public void ChangeAnimState(MonsterAnimState animState)
    {
        monsterAnimController.ChangeAnimState(animState);
    }

    public void RefreshAnim()
    {
        spriteRenderer.material.color = new Color(1, 1, 1, 1);
        monsterAnimController.animator.speed = 1;
    }

    public void SetFrozen()
    {
        IsFrozen = true;
        IsBurn = false;
        burnDamage = 0;

        FrozenAnim();
    }

    public void FrozenAnim()
    {
        RefreshAnim();
        spriteRenderer.material.color = new Color(0.2f, 0.2f, 1, 1);
        monsterAnimController.animator.speed = 0;
    }

    public void SetStun(float duration)
    {
        if ((IsStun && (maxStunTime - curStunTime > duration)) || IsDead) return;

        IsStun = true;
        curStunTime = 0;
        maxStunTime = duration;
        if (stunEffect == null)
            stunEffect = battleManager.effectController.SetEffect(3, transform.position); // ¿”Ω√ ¿Ã∆Â∆Æ
        stunEffect.SetActive(true);
    }

    public void StunTimeUpdate()
    {
        if (curStunTime < maxStunTime)
        {
            curStunTime += Time.deltaTime;
        }
        else
        {
            IsStun = false;
            stunEffect.SetActive(false);
            curStunTime = 0;
            stateMachine.ChangeState(stateMachine.readyState);
        }
    }

    public void SetBurn(int burnDamage, float damageInterval, int burnCount)
    {
        if (this.burnDamage <= burnDamage)
        {
            IsFrozen = false;
            IsBurn = true;
            curBurnTime = 0;
            this.burnDamage = burnDamage;
            this.burnInterval = damageInterval;
            this.burnCount = burnCount;

            BurnAnim();
        }
    }
    public void BurnAnim()
    {
        RefreshAnim();
        spriteRenderer.material.color = new Color(1, 0.4f, 0.4f, 1);
        monsterAnimController.animator.speed = 1.3f;
    }

    public void BurnUpdate()
    {
        if (IsBurn)
        {
            if (curBurnTime < burnInterval)
                curBurnTime += Time.deltaTime;
            else
            {
                curBurnTime = 0;
                --burnCount;
                ChangeHP(-burnDamage);
                if (IsDead)
                    battleManager.BattleOverCheck();
                if (burnCount <= 0)
                {
                    IsBurn = false;
                    burnDamage = 0;
                    RefreshAnim();
                }
            }
        }
    }

}
