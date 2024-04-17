using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.GraphicsBuffer;

public class CharacterActionState : CharacterBaseState
{
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    public CharacterActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AtkUpdate("공격중");

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
                character.StartCoroutine(DoubleAttack(1, 0)); break;
            case 2: // 나뭇가지
                character.StartCoroutine(Heal(30, 1, 1, 0)); break;
            case 3: // 낡은 고서
                character.StartCoroutine(StraightRangeAttack(1, 1, 1, 0)); break;
            case 4: // 푸른 장미
                character.StartCoroutine(AllAttack(1, 4, 0)); break;
            case 5:
                character.StartCoroutine(SpeedUpBuff(300, 4)); break;
            case 6:
                character.StartCoroutine(Attack(1, 10, 2)); break;
            case 7:
                character.StartCoroutine(FrozenAttack(1, 1)); break;
            case 8:
                character.StartCoroutine(StunAttack(1, 10, 0)); break;
            case 9:
                character.StartCoroutine(FlameAttack(1, 5, 0.2f, 20, 6)); break;
            case 10:
                character.StartCoroutine(CrossRangeAttack(1, 1, 0)); break;
            case 11:
                character.StartCoroutine(AllDirectionRangeAttack(1, 1, 0)); break;
            case 12:
                character.StartCoroutine(DoubleRepeatAttack(1, 10, 2)); break;
            case 13:
                character.StartCoroutine(DoubleStraightRangeAttack(1, 1, 0)); break;
            case 14:
                character.StartCoroutine(DoubleFrozenAttack(1, 1)); break;
            case 15:
                character.StartCoroutine(DoubleStunAttack(1, 10, 0)); break;
            case 16:
                character.StartCoroutine(DoubleFlameAttack(1, 5, 0.2f, 20, 6)); break;
            case 17:
                character.StartCoroutine(StraightRangeFrozenAttack(1, 1, 0)); break;
            case 18:
                character.StartCoroutine(StraightRangeStunAttack(1, 10, 1, 0)); break;
            case 19:
                character.StartCoroutine(StraightRangeFlameAttack(1, 5, 0.2f, 20, 1, 0)); break;
            default:
                character.StartCoroutine(Attack(1, 1, 0)); break;
        }
    }

    IEnumerator AttackBase(Monster target, int damage, int effectId)
    {
        battleManager.battleCanvas.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 공격!\n{target.monsterName}에게 {damage}의 피해!");
    }

    IEnumerator Attack(int damageMultiple, int count, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for (int i = 0; i < count; i++)
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

        for (int j = 0; j < battleManager.monsters.Count; j++)
        {
            if (!battleManager.monsters[j].IsDead)
            {
                battleManager.monsters[j].ChangeHP(-damage);
                battleManager.battleCanvas.SetRepeatEffect(effectId, battleManager.monsters[j].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
    }

    IEnumerator AllAttack(int damageMultiple, int count, int effectId)
    {
        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < count; i++)
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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

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

    IEnumerator Heal(int value, int damageMultiple, int count, int effectId)
    {
        battleManager.battleCanvas.SetRepeatEffect(5, character.transform.position); // 임시 이펙트
        character.ChangeHP(value);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}이 {battleManager.rouletteEquip[0].data.tripleValue}의 체력을 회복!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        character.StartCoroutine(Attack(damageMultiple, count, effectId));
    }

    IEnumerator StraightRangeAttack(int damageMultiple, int count, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
        for (int i = 0; i < count; i++)
        {
            yield return StraightRangeAttackBase(target, hit, damage, effectId);
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

        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
    }

    IEnumerator CrossRangeAttack(int damageMultiple, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        Vector3 leftPos = character.transform.position + (Vector3.left * 1.5f) + (Vector3.left * 2.5f * range);
        Vector3 rightPos = leftPos + (Vector3.right * 5f) + (Vector3.right * 5f * range);
        Vector3 upPos = character.transform.position + Vector3.right + (Vector3.up * 2.5f) + (Vector3.up * 2.5f * range);
        Vector3 downPos = upPos + (Vector3.down * 5f) + (Vector3.down * 5f * range);

        RaycastHit2D[] horizontalHit;
        horizontalHit = Physics2D.RaycastAll(leftPos, Vector3.right, 5f + (5f * range));
        RaycastHit2D[] verticalHit;
        verticalHit = Physics2D.RaycastAll(upPos, Vector3.down, 5f + (5f * range));

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

        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position, leftPos); // 임시 이펙트
        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position, upPos);
        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position, rightPos);
        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position, downPos);

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllDirectionRangeAttack(int damageMultiple, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        Vector3 attackRange = new Vector3(2 + (3 * range), 2 + (3 * range));
        hit = Physics2D.BoxCastAll(selectMonsterPosition + (Vector3.up / 2), attackRange, 0, Vector3.zero);
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

        battleManager.battleCanvas.SetRepeatEffect(effectId, range * 4, target.transform.position); // 임시 이펙트

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
        yield return waitForEndOfFrame;

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator SpeedUpBuff(int value, int turnCount)
    {
        battleManager.battleCanvas.SetRepeatEffect(4, character.transform.position); // 임시 이펙트

        character.characterBuffHandler.AddBuff(BuffType.Speed, value, turnCount);

        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 속도가 빨라졌다!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        character.StartCoroutine(Attack(1, 1, 2));
    }

    IEnumerator FrozenAttack(int damageMultiple, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return (AttackBase(target, damage, effectId));
        target.SetFrozen();

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StunAttack(int damageMultiple, float duration, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return AttackBase(target, damage, effectId);
        target.SetStun(duration);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return AttackBase(target, damage, effectId);
        target.SetBurn(burnDamage, damageInterval, burnCount);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleRepeatAttack(int damageMultiple, int count, int effectId)
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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            for (int i = 0; i < count; i++)
            {
                yield return AttackBase(target, damage, effectId);
                if (target.IsDead) break;
            }

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            for (int i = 0; i < count; i++)
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
            character.StartCoroutine(Attack(damageMultiple, count, effectId));
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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

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

            int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

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

    IEnumerator StraightRangeFrozenAttack(int damageMultiple, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
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
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 빙결 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StraightRangeStunAttack(int damageMultiple, float duration, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
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
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 스턴 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StraightRangeFlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
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
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

}
