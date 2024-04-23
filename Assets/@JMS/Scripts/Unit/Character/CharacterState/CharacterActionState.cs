using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CharacterActionState : CharacterBaseState
{
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    WaitForSeconds waitForSeconds = new WaitForSeconds(0.4f);

    public CharacterActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        StateUpdate("공격중");

        if (battleManager.rouletteResult == RouletteResult.Triple)
        {
            ItemSkill(battleManager.rouletteEquip[0].data.id);
        }
        else if (battleManager.rouletteResult == RouletteResult.Cheat)
        {
            ItemSkill(battleManager.cheatItemId);
        }
        else
        {
            character.StartCoroutine(Attack(1, 1, 0));
        }
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }

    void ItemSkill(int itemID)
    {
        switch (itemID)
        {
            case 0: // 정의의 주먹
                character.StartCoroutine(Attack(1, 1, 0)); break;
            case 1: // 체스말(폰)
                character.StartCoroutine(Attack(1, 10, 2)); break;
            case 2: // 나뭇가지
                character.StartCoroutine(Heal(30, 1, 1, 0)); break;
            case 3: // 낡은 고서
                character.StartCoroutine(SpeedUpBuff(300, 4)); break;
            case 4: // 푸른 장미
                character.StartCoroutine(AllAttack(1, 1, 0)); break;
            case 5:
                character.StartCoroutine(DoubleAttack(1, 0)); break;
            case 6:
                character.StartCoroutine(FlameAttack(1, 5, 0.2f, 20, 6)); break;
            case 7:
                character.StartCoroutine(FrozenAttack(1, 1)); break;
            case 8:
                character.StartCoroutine(StunAttack(1, 10, 10)); break;
            case 9:
                character.StartCoroutine(StraightRangeAttack(1, 1, 1, 0)); break;
            case 10:
                character.StartCoroutine(CrossRangeAttack(1, 1, 1, 0)); break;
            case 11:
                character.StartCoroutine(AllDirectionRangeAttack(1, 1, 1, 0)); break;
            case 12:
                character.StartCoroutine(DoubleRepeatAttack(1, 10, 2)); break;
            case 13:
                character.StartCoroutine(DoubleStraightRangeAttack(1, 1, 0)); break;
            case 14:
                character.StartCoroutine(DoubleFrozenAttack(1, 1)); break;
            case 15:
                character.StartCoroutine(DoubleStunAttack(1, 10, 10)); break;
            case 16:
                character.StartCoroutine(DoubleFlameAttack(1, 5, 0.2f, 20, 6)); break;
            case 17:
                character.StartCoroutine(DoubleCrossRangeAttack(1, 1, 0)); break;
            case 18:
                character.StartCoroutine(DoubleAllDirectionRangeAttack(1, 1, 0)); break;
            case 19:
                character.StartCoroutine(StraightRangeFrozenAttack(1, 1, 1, 0)); break;
            case 20:
                character.StartCoroutine(StraightRangeStunAttack(1, 1, 10, 1, 0)); break;
            case 21:
                character.StartCoroutine(StraightRangeFlameAttack(1, 1, 5, 0.2f, 20, 1, 0)); break;
            case 22:
                character.StartCoroutine(StraightRangeAttack(1, 10, 1, 0)); break;
            case 23:
                character.StartCoroutine(CrossRangeFrozenAttack(1, 1, 1, 0)); break;
            case 24:
                character.StartCoroutine(CrossRangeStunAttack(1, 1, 10, 1, 0)); break;
            case 25:
                character.StartCoroutine(CrossRangeFlameAttack(1, 1, 5, 0.2f, 20, 1, 0)); break;
            case 26:
                character.StartCoroutine(CrossRangeAttack(1, 10, 1, 0)); break;
            case 27:
                character.StartCoroutine(AllDirectionRangeFrozenAttack(1, 1, 1, 11)); break;
            case 28:
                character.StartCoroutine(AllDirectionRangeStunAttack(1, 1, 10, 1, 10)); break;
            case 29:
                character.StartCoroutine(AllDirectionRangeFlameAttack(1, 1, 5, 0.2f, 20, 1, 12)); break;
            case 30:
                character.StartCoroutine(AllDirectionRangeAttack(1, 10, 1, 0)); break;
            case 31:
                character.StartCoroutine(AllFrozenAttack(1, 1, 1)); break;
            case 32:
                character.StartCoroutine(AllStunAttack(1, 1, 10, 0)); break;
            case 33:
                character.StartCoroutine(AllFlameAttack(1, 1, 5, 0.2f, 20, 6)); break;
            case 34:
                character.StartCoroutine(AllAttack(1, 10, 0)); break;
            case 35:
                character.StartCoroutine(DeathAttack(1)); break;
            case 36:
                character.StartCoroutine(AllFrozenAndDeathAttack(1)); break;
            case 37:
                character.StartCoroutine(AllStunAndDeathAttack(10, 1)); break;
            case 38:
                character.StartCoroutine(AllFlameAndDeathAttack(5, 0.2f, 20, 1)); break;
                //case 39:
                //
            case 40:
                character.StartCoroutine(DrainHpAttack(1, 1, 50, 0)); break;
            case 41:
                character.StartCoroutine(ArrowAttack(1, 1, 1, 7)); break;
            case 42:
                character.StartCoroutine(FrozenArrowAttack(1, 1, 1, 9)); break;
            case 43:
                character.StartCoroutine(StunArrowAttack(1, 10, 1, 1, 7)); break;
            case 44:
                character.StartCoroutine(FlameArrowAttack(1, 30, 0.5f, 10, 1, 1, 8)); break;
            case 45:
                character.StartCoroutine(AllDirectionRangeArrowAttack(1, 1, 1, 1, 7, 0)); break;
            case 46:
                character.StartCoroutine(AllDirectionRangeFrozenArrowAttack(1, 1, 1, 1, 9, 11)); break;
            case 47:
                character.StartCoroutine(AllDirectionRangeStunArrowAttack(1, 10, 1, 1, 1, 7, 10)); break;
            case 48:
                character.StartCoroutine(AllDirectionRangeFlameArrowAttack(1, 30, 0.5f, 10, 1, 1, 1, 8, 12)); break;
            case 49:
                character.StartCoroutine(ArrowAttack(1, 2, 4, 7)); break;
            case 50:
                character.StartCoroutine(ArrowAttack(1, 10, 2, 7)); break;
            case 51:
                character.StartCoroutine(AllDirectionRangeArrowAttack(1, 1, 6, 1, 7, 0)); break;
            case 52:
                character.StartCoroutine(AllDirectionRangeStunArrowAttack(1, 10, 1, 2, 1, 7, 10)); break;
            case 53:
                character.StartCoroutine(AllDirectionRangeFrozenArrowAttack(1, 1, 2, 1, 9, 11)); break;
            case 54:
                character.StartCoroutine(AllDirectionRangeFlameArrowAttack(1, 30, 0.5f, 10, 1, 2, 1, 8, 12)); break;
                //case 55:
                //
            case 56:
                character.StartCoroutine(DeathAttack(4)); break;
            default:
                character.StartCoroutine(Attack(1, 1, 0)); break;
        }
    }

    IEnumerator AttackBase(Monster target, int damage, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkRangeHitSFX(); // 임시 사운드
        battleManager.effectController.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 공격!\n{target.monsterName}에게 {damage}의 피해!");
    }

    IEnumerator Attack(int damageMultiple, int attackCount, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return AttackBase(target, damage, effectId);
            if (target.IsDead) break;
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttackBase(int damage, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit1SFX(); // 임시 사운드

        for (int j = 0; j < battleManager.monsters.Count; j++)
        {
            if (!battleManager.monsters[j].IsDead)
            {
                battleManager.monsters[j].ChangeHP(-damage);
                battleManager.effectController.SetRepeatEffect(effectId, battleManager.monsters[j].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllFrozenAttackBase(int damage, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAttack1SFX(); // 임시 사운드

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.monsters[i].ChangeHP(-damage);
                battleManager.monsters[i].SetFrozen();
                battleManager.effectController.SetRepeatEffect(effectId, battleManager.monsters[i].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllStunAttackBase(int damage, int duration, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAttack1SFX(); // 임시 사운드

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.monsters[i].ChangeHP(-damage);
                battleManager.monsters[i].SetStun(duration);
                battleManager.effectController.SetRepeatEffect(effectId, battleManager.monsters[i].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllFlameAttackBase(int damage, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAttack1SFX(); // 임시 사운드

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.monsters[i].ChangeHP(-damage);
                battleManager.monsters[i].SetBurn(burnDamage, damageInterval, burnCount);
                battleManager.effectController.SetRepeatEffect(effectId, battleManager.monsters[i].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllAttack(int damageMultiple, int attackCount, int effectId)
    {
        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllAttackBase(damage, effectId);

            if (battleManager.StageClearCheck())
                break;
        }
        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack(int damageMultiple, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return AttackBase(target, damage, effectId);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return AttackBase(nextTarget, damage, effectId);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(Attack(damageMultiple, 1, effectId));
        }
    }

    IEnumerator Heal(int healValue, int damageMultiple, int attackCount, int effectId)
    {
        battleManager.effectController.SetRepeatEffect(5, character.transform.position); // 임시 이펙트
        character.ChangeHP(healValue);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}이 {battleManager.rouletteEquip[0].data.tripleValue}의 체력을 회복!");

        AudioManager.Instance.PlayMonsterJumpSFX(); // 임시 사운드
        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        character.StartCoroutine(Attack(damageMultiple, attackCount, effectId));
    }

    IEnumerator StraightRangeAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
        for (int i = 0; i < attackCount; i++)
        {
            yield return StraightRangeAttackBase(target, hit, damage, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StraightRangeAttackBase(Monster target, RaycastHit2D[] hit, int damage, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[j].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
    }

    IEnumerator StraightRangeFrozenAttackBase(Monster target, RaycastHit2D[] hit, int damage, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[j].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetFrozen();
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
    }

    IEnumerator StraightRangeStunAttackBase(Monster target, RaycastHit2D[] hit, int damage, float duration, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[j].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetStun(duration);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
    }

    IEnumerator StraightRangeFlameAttackBase(Monster target, RaycastHit2D[] hit, int damage, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        for (int j = 0; j < hit.Length; j++)
        {
            if (hit[j].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[j].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetBurn(burnDamage, damageInterval, burnCount);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
    }
    IEnumerator CrossRangeAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return CrossRangeAttackBase(target, damage, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator CrossRangeAttackBase(Monster target, int damage, int range, int effectId)
    {
        Vector3 leftPos = target.transform.position + (Vector3.left * 2.5f * range);
        Vector3 rightPos = target.transform.position + (Vector3.right * 2.5f * range);
        Vector3 upPos = target.transform.position + (Vector3.up * 2.5f * range);
        Vector3 downPos = target.transform.position + (Vector3.down * 2.5f * range);

        RaycastHit2D[] horizontalHit;
        horizontalHit = Physics2D.RaycastAll(leftPos, Vector3.right, 5f * range);
        RaycastHit2D[] verticalHit;
        verticalHit = Physics2D.RaycastAll(upPos, Vector3.down, 5f * range);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, leftPos); // 임시 이펙트
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, upPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, rightPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, downPos);

        for (int i = 0; i < horizontalHit.Length; i++)
        {
            if (horizontalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = horizontalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        for (int i = 0; i < verticalHit.Length; i++)
        {
            if (verticalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = verticalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 십자 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator CrossRangeFrozenAttackBase(Monster target, int damage, int range, int effectId)
    {
        Vector3 leftPos = target.transform.position + (Vector3.left * 2.5f * range);
        Vector3 rightPos = target.transform.position + (Vector3.right * 2.5f * range);
        Vector3 upPos = target.transform.position + (Vector3.up * 2.5f * range);
        Vector3 downPos = target.transform.position + (Vector3.down * 2.5f * range);

        RaycastHit2D[] horizontalHit;
        horizontalHit = Physics2D.RaycastAll(leftPos, Vector3.right, 5f * range);
        RaycastHit2D[] verticalHit;
        verticalHit = Physics2D.RaycastAll(upPos, Vector3.down, 5f * range);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, leftPos); // 임시 이펙트
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, upPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, rightPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, downPos);

        for (int i = 0; i < horizontalHit.Length; i++)
        {
            if (horizontalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = horizontalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetFrozen();
                }
            }
        }
        for (int i = 0; i < verticalHit.Length; i++)
        {
            if (verticalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = verticalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetFrozen();
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 십자 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }
    IEnumerator CrossRangeStunAttackBase(Monster target, int damage, int duration, int range, int effectId)
    {
        Vector3 leftPos = target.transform.position + (Vector3.left * 2.5f * range);
        Vector3 rightPos = target.transform.position + (Vector3.right * 2.5f * range);
        Vector3 upPos = target.transform.position + (Vector3.up * 2.5f * range);
        Vector3 downPos = target.transform.position + (Vector3.down * 2.5f * range);

        RaycastHit2D[] horizontalHit;
        horizontalHit = Physics2D.RaycastAll(leftPos, Vector3.right, 5f * range);
        RaycastHit2D[] verticalHit;
        verticalHit = Physics2D.RaycastAll(upPos, Vector3.down, 5f * range);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, leftPos); // 임시 이펙트
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, upPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, rightPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, downPos);

        for (int i = 0; i < horizontalHit.Length; i++)
        {
            if (horizontalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = horizontalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetStun(duration);
                }
            }
        }
        for (int i = 0; i < verticalHit.Length; i++)
        {
            if (verticalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = verticalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetStun(duration);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 십자 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }
    IEnumerator CrossRangeFlameAttackBase(Monster target, int damage, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        Vector3 leftPos = target.transform.position + (Vector3.left * 2.5f * range);
        Vector3 rightPos = target.transform.position + (Vector3.right * 2.5f * range);
        Vector3 upPos = target.transform.position + (Vector3.up * 2.5f * range);
        Vector3 downPos = target.transform.position + (Vector3.down * 2.5f * range);

        RaycastHit2D[] horizontalHit;
        horizontalHit = Physics2D.RaycastAll(leftPos, Vector3.right, 5f * range);
        RaycastHit2D[] verticalHit;
        verticalHit = Physics2D.RaycastAll(upPos, Vector3.down, 5f * range);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkMagicSFX(); // 임시 사운드

        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, leftPos); // 임시 이펙트
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, upPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, rightPos);
        battleManager.effectController.SetMoveEffect(effectId, target.transform.position, downPos);

        for (int i = 0; i < horizontalHit.Length; i++)
        {
            if (horizontalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = horizontalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetBurn(burnDamage, damageInterval, burnCount);
                }
            }
        }
        for (int i = 0; i < verticalHit.Length; i++)
        {
            if (verticalHit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = verticalHit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetBurn(burnDamage, damageInterval, burnCount);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 십자 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }
    IEnumerator AllDirectionRangeAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllDirectionRangeAttackBase(target, damage, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeAttackBase(Monster target, int damage, int range, int effectId)
    {
        RaycastHit2D[] hit;
        Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
        hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit1SFX(); // 임시 사운드

        battleManager.effectController.SetRepeatEffect(effectId, range * 4, target.transform.position); // 임시 이펙트

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 범위 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllDirectionRangeFrozenAttackBase(Monster target, int damage, int range, int effectId)
    {
        RaycastHit2D[] hit;
        Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
        hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit1SFX(); // 임시 사운드

        battleManager.effectController.SetRepeatEffect(effectId, range * 4, target.transform.position); // 임시 이펙트

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetFrozen();
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 범위 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllDirectionRangeStunAttackBase(Monster target, int damage, int duration, int range, int effectId)
    {
        RaycastHit2D[] hit;
        Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
        hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit1SFX(); // 임시 사운드

        battleManager.effectController.SetRepeatEffect(effectId, range * 4, target.transform.position); // 임시 이펙트

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetStun(duration);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 범위 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllDirectionRangeFlameAttackBase(Monster target, int damage, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        RaycastHit2D[] hit;
        Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
        hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit1SFX(); // 임시 사운드

        battleManager.effectController.SetRepeatEffect(effectId, range * 4, target.transform.position); // 임시 이펙트

        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                {
                    hitMonster.ChangeHP(-damage);
                    hitMonster.SetBurn(burnDamage, damageInterval, burnCount);
                }
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 범위 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator SpeedUpBuff(int speedUpValue, int turnCount)
    {
        battleManager.effectController.SetRepeatEffect(4, character.transform.position); // 임시 이펙트

        character.characterBuffController.AddBuff(BuffType.SPD, "아이템 스킬 버프", speedUpValue, turnCount);

        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 속도가 빨라졌다!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayHit2SFX(); // 임시 사운드

        character.StartCoroutine(Attack(1, 1, 2));
    }

    IEnumerator FrozenAttack(int damageMultiple, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return (AttackBase(target, damage, effectId));
        target.SetFrozen();

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StunAttack(int damageMultiple, float stunDuration, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return AttackBase(target, damage, effectId);
        target.SetStun(stunDuration);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return AttackBase(target, damage, effectId);
        target.SetBurn(burnDamage, damageInterval, burnCount);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleRepeatAttack(int damageMultiple, int attackCount, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            for (int i = 0; i < attackCount; i++)
            {
                yield return AttackBase(target, damage, effectId);
                if (target.IsDead) break;
            }

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            for (int i = 0; i < attackCount; i++)
            {
                yield return AttackBase(nextTarget, damage, effectId);
                if (nextTarget.IsDead) break;
            }

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(Attack(damageMultiple, attackCount, effectId));
        }
    }

    IEnumerator DoubleStraightRangeAttack(int damageMultiple, int range, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);
            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            RaycastHit2D[] hit;
            hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
            yield return StraightRangeAttackBase(target, hit, damage, effectId);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
            yield return StraightRangeAttackBase(nextTarget, hit, damage, effectId);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(StraightRangeAttack(damageMultiple, 1, range, effectId));
        }
    }

    IEnumerator DoubleFrozenAttack(int damageMultiple, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return AttackBase(target, damage, effectId);
            target.SetFrozen();

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return (AttackBase(nextTarget, damage, effectId));
            nextTarget.SetFrozen();

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(FrozenAttack(damageMultiple, effectId));
        }
    }

    IEnumerator DoubleStunAttack(int damageMultiple, float duration, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return AttackBase(target, damage, effectId);
            target.SetStun(duration);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return AttackBase(nextTarget, damage, effectId);
            nextTarget.SetStun(duration);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(StunAttack(damageMultiple, duration, effectId));
        }
    }

    IEnumerator DoubleFlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return AttackBase(target, damage, effectId);
            target.SetBurn(burnDamage, damageInterval, burnCount);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return AttackBase(nextTarget, damage, effectId);
            nextTarget.SetBurn(burnDamage, damageInterval, burnCount);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(FlameAttack(damageMultiple, burnDamage, damageInterval, burnCount, effectId));
        }
    }

    IEnumerator DoubleCrossRangeAttack(int damageMultiple, int range, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return CrossRangeAttackBase(target, damage, range, effectId);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return CrossRangeAttackBase(nextTarget, damage, range, effectId);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(CrossRangeAttack(damageMultiple, 1, range, effectId));
        }
    }

    IEnumerator DoubleAllDirectionRangeAttack(int damageMultiple, int range, int effectId)
    {
        if (battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = target.transform.position + Vector3.left;
            Vector3 nextMonsterPosition = nextTarget.transform.position + Vector3.left;

            int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return AllDirectionRangeAttackBase(target, damage, range, effectId);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return AllDirectionRangeAttackBase(nextTarget, damage, range, effectId);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(AllDirectionRangeAttack(damageMultiple, 1, range, effectId));
        }
    }

    IEnumerator StraightRangeFrozenAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
        for (int i = 0; i < attackCount; i++)
        {
            yield return StraightRangeFrozenAttackBase(target, hit, damage, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StraightRangeStunAttack(int damageMultiple, int attackCount, float duration, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
        for (int i = 0; i < attackCount; i++)
        {
            yield return StraightRangeStunAttackBase(target, hit, damage, duration, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StraightRangeFlameAttack(int damageMultiple, int attackCount, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
        for (int i = 0; i < attackCount; i++)
        {
            yield return StraightRangeFlameAttackBase(target, hit, damage, burnDamage, damageInterval, burnCount, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator CrossRangeFrozenAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return CrossRangeFrozenAttackBase(target, damage, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator CrossRangeStunAttack(int damageMultiple, int attackCount, int duration, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return CrossRangeStunAttackBase(target, damage, duration, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator CrossRangeFlameAttack(int damageMultiple, int attackCount, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return CrossRangeFlameAttackBase(target, damage, burnDamage, damageInterval, burnCount, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeFrozenAttack(int damageMultiple, int attackCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllDirectionRangeFrozenAttackBase(target, damage, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
    IEnumerator AllDirectionRangeStunAttack(int damageMultiple, int attackCount, int duration, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllDirectionRangeStunAttackBase(target, damage, duration, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeFlameAttack(int damageMultiple, int attackCount, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllDirectionRangeFlameAttackBase(target, damage, burnDamage, damageInterval, burnCount, range, effectId);
            if (target.IsDead) { break; }
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllFrozenAttack(int damageMultiple, int attackCount, int effectId)
    {
        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllFrozenAttackBase(damage, effectId);

            if (battleManager.StageClearCheck())
                break;
        }
        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllStunAttack(int damageMultiple, int attackCount, int duration, int effectId)
    {
        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllStunAttackBase(damage, duration, effectId);

            if (battleManager.StageClearCheck())
                break;
        }
        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllFlameAttack(int damageMultiple, int attackCount, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < attackCount; i++)
        {
            yield return AllFlameAttackBase(damage, burnDamage, damageInterval, burnCount, effectId);

            if (battleManager.StageClearCheck())
                break;
        }
        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DeathAttack(int attackCount)
    {
        Monster target = battleManager.selectMonster;

        for (int i = 0; i < attackCount; i++)
        {
            Vector3 startPos = character.transform.position;
            while (target.IsDead)
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            Vector3 moveDirection = Vector3.Normalize(target.transform.position - startPos);
            Vector3 targetPos = target.transform.position + moveDirection;
            character.spriteRenderer.flipX = moveDirection.x < 0 ? true : false;

            character.ChangeAnimState(CharacterAnimState.Running);
            float curMoveTime = 0;
            while (LerpCharacter(startPos, targetPos, curMoveTime)) { curMoveTime += Time.deltaTime * 50f; yield return null; }

            battleManager.effectController.SetRepeatEffect(13, target.startPosition); // 임시 이펙트

            character.PlayAnim(CharacterAnim.Shot);
            while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
            character.PlayAnim(CharacterAnim.Idle);
            yield return waitForEndOfFrame;

            target.ChangeHP(-9999);
            battleManager.battleCanvas.UpdateBattleText("!!");

            if (battleManager.StageClearCheck()) { break; }
        }

        character.spriteRenderer.flipX = false;

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllFrozenAndDeathAttack(int attackCount)
    {
        character.PlayAnim(CharacterAnim.Fire2H);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Fire2H")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.effectController.SetRepeatEffect(1, battleManager.monsters[i].startPosition); // 임시 이펙트
                battleManager.monsters[i].SetFrozen();
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"빙결");
        yield return waitForSeconds;

        character.StartCoroutine(DeathAttack(attackCount));
    }

    IEnumerator AllStunAndDeathAttack(int stunDuration, int attackCount)
    {
        character.PlayAnim(CharacterAnim.Fire2H);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Fire2H")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.effectController.SetRepeatEffect(10, battleManager.monsters[i].startPosition); // 임시 이펙트
                battleManager.monsters[i].SetStun(stunDuration);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"스턴");
        yield return waitForSeconds;

        character.StartCoroutine(DeathAttack(attackCount));
    }

    IEnumerator AllFlameAndDeathAttack(int burnDamage, float damageInterval, int burnCount, int attackCount)
    {
        character.PlayAnim(CharacterAnim.Fire2H);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Fire2H")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.effectController.SetRepeatEffect(6, battleManager.monsters[i].startPosition); // 임시 이펙트
                battleManager.monsters[i].SetBurn(burnDamage, damageInterval, burnCount);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"화염");
        yield return waitForSeconds;

        character.StartCoroutine(DeathAttack(attackCount));
    }

    IEnumerator DrainHpAttack(int damageMultiple, int attackCount, int drainPercent, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < attackCount; i++)
        {
            yield return DrainHpAttackBase(target, damage, drainPercent, effectId);
            if (target.IsDead) break;
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DrainHpAttackBase(Monster target, int damage, int drainPercent, int effectId)
    {
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        AudioManager.Instance.PlayAtkRangeHitSFX(); // 임시 사운드
        battleManager.effectController.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
        target.ChangeHP(-damage);
        character.ChangeHP((int)(damage * (drainPercent / 100f)));
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 흡혈 공격!\n{target.monsterName}에게 {damage}의 피해!");
    }

    IEnumerator ArrowAttack(int damageMultiple, int attackCount, int targetCount, int arrowEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 화살 공격!\n{target.monsterName}에게 {damage}의 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FrozenArrowAttack(int damageMultiple, int attackCount, int targetCount, int arrowEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);
                target.SetFrozen();
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 얼음 화살!\n{target.monsterName}에게 {damage}의 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StunArrowAttack(int damageMultiple, int stunDuration, int attackCount, int targetCount, int arrowEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);
                target.SetStun(stunDuration);
                battleManager.effectController.SetRepeatEffect(10, target.transform.position); // 임시 이펙트
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 기절 화살!\n{target.monsterName}에게 {damage}의 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FlameArrowAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int attackCount, int targetCount, int arrowEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);
                target.SetBurn(burnDamage, damageInterval, burnCount);
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 화염 화살!\n{target.monsterName}에게 {damage}의 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeArrowAttack(int damageMultiple, int attackCount, int targetCount, int range, int arrowEffectId, int rangeAttackEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            RaycastHit2D[] hit;
            Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
            hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);

                for (int k = 0; k < hit.Length; k++)
                {
                    if (hit[k].collider.CompareTag("Monster"))
                    {
                        Monster hitMonster = hit[k].collider.GetComponent<Monster>();
                        if (!hitMonster.IsDead)
                            hitMonster.ChangeHP(-damage);
                    }
                }
                battleManager.effectController.SetRepeatEffect(rangeAttackEffectId, range * 4, target.transform.position); // 임시 이펙트
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 폭발 화살!\n{damage} 의 범위 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeFrozenArrowAttack(int damageMultiple, int attackCount, int targetCount, int range, int arrowEffectId, int rangeAttackEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            RaycastHit2D[] hit;
            Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
            hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);

                for (int k = 0; k < hit.Length; k++)
                {
                    if (hit[k].collider.CompareTag("Monster"))
                    {
                        Monster hitMonster = hit[k].collider.GetComponent<Monster>();
                        if (!hitMonster.IsDead)
                        {
                            hitMonster.ChangeHP(-damage);
                            hitMonster.SetFrozen();
                        }
                    }
                }
                battleManager.effectController.SetRepeatEffect(rangeAttackEffectId, range * 4, target.transform.position); // 임시 이펙트
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 얼음 폭발 화살!\n{damage} 의 범위 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeStunArrowAttack(int damageMultiple, int stunDuration, int attackCount, int targetCount, int range, int arrowEffectId, int rangeAttackEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            RaycastHit2D[] hit;
            Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
            hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);

                for (int k = 0; k < hit.Length; k++)
                {
                    if (hit[k].collider.CompareTag("Monster"))
                    {
                        Monster hitMonster = hit[k].collider.GetComponent<Monster>();
                        if (!hitMonster.IsDead)
                        {
                            hitMonster.ChangeHP(-damage);
                            hitMonster.SetStun(stunDuration);
                        }
                    }
                }
                battleManager.effectController.SetRepeatEffect(rangeAttackEffectId, range * 4, target.transform.position); // 임시 이펙트
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 기절 폭발 화살!\n{damage} 의 범위 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeFlameArrowAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount,
        int attackCount, int targetCount, int range, int arrowEffectId, int rangeAttackEffectId)
    {
        Monster target = battleManager.selectMonster;
        bool[] IsDamaged = new bool[battleManager.monsters.Count];

        int damage = battleManager.GetRouletteValue(character.addBuffAtk) * damageMultiple;

        targetCount = targetCount > battleManager.monsters.Count ? battleManager.monsters.Count : targetCount;

        for (int i = 0; i < targetCount; i++)
        {
            while (target.IsDead || IsDamaged[target.monsterNumber])
            {
                target = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            IsDamaged[target.monsterNumber] = true;

            Vector3 arrowStartPos = character.startPosition + Vector3.right;
            Vector3 arrowTargetPos = target.startPosition + Vector3.up / 2;
            float angle = Mathf.Atan2(arrowTargetPos.y - arrowStartPos.y, arrowTargetPos.x - arrowStartPos.x) * Mathf.Rad2Deg;

            RaycastHit2D[] hit;
            Vector3 hitScale = new Vector3(2 + (3 * range), 2 + (3 * range));
            hit = Physics2D.BoxCastAll(target.transform.position + (Vector3.up / 2), hitScale, 0, Vector3.zero);

            for (int j = 0; j < attackCount; j++)
            {
                battleManager.effectController.SetMoveEffect(arrowEffectId, arrowStartPos, arrowTargetPos, angle); // 임시 이펙트
                character.PlayAnim(CharacterAnim.Shot);
                while (1f > GetNormalizedTime(character.animatorController.animator, "Shot")) { yield return null; }
                character.PlayAnim(CharacterAnim.Idle);
                yield return waitForEndOfFrame;

                target.ChangeHP(-damage);

                for (int k = 0; k < hit.Length; k++)
                {
                    if (hit[k].collider.CompareTag("Monster"))
                    {
                        Monster hitMonster = hit[k].collider.GetComponent<Monster>();
                        if (!hitMonster.IsDead)
                        {
                            hitMonster.ChangeHP(-damage);
                            hitMonster.SetBurn(burnDamage, damageInterval, burnCount);
                        }
                    }
                }
                battleManager.effectController.SetRepeatEffect(rangeAttackEffectId, range * 4, target.transform.position); // 임시 이펙트
                battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 화염 폭발 화살!\n{damage} 의 범위 피해!");

                if (target.IsDead) break;
            }

            if (battleManager.StageClearCheck()) { break; }
        }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
}
