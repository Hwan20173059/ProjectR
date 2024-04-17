using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterActionState : CharacterBaseState
{
    WaitForSeconds waitForAttack = new WaitForSeconds(0.3f);

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
                character.StartCoroutine(StraightRangeAttack(1, 1, 0)); break;
            case 4: // 푸른 장미
                character.StartCoroutine(AllAttack(1, 4, 0)); break;
            case 5:
                character.StartCoroutine(SpeedUpBuff(300, 4)); break;
            case 6:
                character.StartCoroutine(Attack(1, 10, 2)); break;
            case 7:
                character.StartCoroutine(FrozenAttack(1)); break;
            case 8:
                character.StartCoroutine(StunAttack(1, 10)); break;
            case 9:
                character.StartCoroutine(FlameAttack(1, 5, 0.2f, 20)); break;
            case 10:
                character.StartCoroutine(CrossRangeAttack(1, 1, 0)); break;
            case 11:
                character.StartCoroutine(AllDirectionRangeAttack(1, 1, 0)); break;
            case 12:
                character.StartCoroutine(DoubleRepeatAttack(1, 10, 2)); break;
            case 13:
                character.StartCoroutine(DoubleStraightRangeAttack(1, 1, 0)); break;
            case 14:
                character.StartCoroutine(DoubleFrozenAttack(1)); break;
            case 15:
                character.StartCoroutine(DoubleStunAttack(1, 10)); break;
            case 16:
                character.StartCoroutine(DoubleFlameAttack(1, 5, 0.2f, 20)); break;
            case 17:
                character.StartCoroutine(FrozenStraightRangeAttack(1, 1, 0)); break;
            case 18:
                character.StartCoroutine(StunStraightRangeAttack(1, 10, 1, 0)); break;
            case 19:
                character.StartCoroutine(FlameStraightRangeAttack(1, 5, 0.2f, 20, 1, 0)); break;
            default:
                character.StartCoroutine(Attack(1, 1, 0)); break;
        }
    }

    IEnumerator AttackBase(Monster target, int damage)
    {
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 공격!\n{target.monsterName}에게 {damage}의 피해!");
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
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
            battleManager.battleCanvas.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
            character.StartCoroutine(AttackBase(target, damage));
            yield return waitForAttack;
            if (target.IsDead) break;
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttackBase(int damage, int effectId)
    {
        for (int j = 0; j < battleManager.monsters.Count; j++)
        {
            if (!battleManager.monsters[j].IsDead)
            {
                battleManager.monsters[j].ChangeHP(-damage);
                battleManager.battleCanvas.SetRepeatEffect(effectId, battleManager.monsters[j].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
    }

    IEnumerator AllAttack(int damageMultiple, int count, int effectId)
    {
        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        for (int i = 0; i < count; i++)
        {
            character.StartCoroutine(AllAttackBase(damage, effectId));
            yield return waitForAttack;

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

            battleManager.battleCanvas.SetRepeatEffect(effectId, target.transform.position);
            yield return character.StartCoroutine(AttackBase(target, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(effectId, nextTarget.transform.position);
            yield return character.StartCoroutine(AttackBase(nextTarget, damage));

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

        character.StartCoroutine(Attack(damageMultiple, count, effectId));
    }

    IEnumerator StraightRangeAttack(int damageMultiple, int range, int effectId)
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
                    hitMonster.ChangeHP(-damage);
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

    IEnumerator FrozenAttack(int damageMultiple)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        battleManager.battleCanvas.SetRepeatEffect(1, target.transform.position); // 임시 이펙트
        yield return character.StartCoroutine(AttackBase(target, damage));
        target.SetFrozen();

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator StunAttack(int damageMultiple, float duration)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return character.StartCoroutine(AttackBase(target, damage));
        target.SetStun(duration);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(character.addBuffAtk) * damageMultiple;

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        battleManager.battleCanvas.SetRepeatEffect(6, target.transform.position); // 임시 이펙트
        yield return character.StartCoroutine(AttackBase(target, damage));
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
                battleManager.battleCanvas.SetRepeatEffect(effectId, target.transform.position); // 임시 이펙트
                character.StartCoroutine(AttackBase(target, damage));
                yield return waitForAttack;
                if (target.IsDead) break;
            }

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            for (int i = 0; i < count; i++)
            {
                battleManager.battleCanvas.SetRepeatEffect(effectId, nextTarget.transform.position); // 임시 이펙트
                character.StartCoroutine(AttackBase(nextTarget, damage));
                yield return waitForAttack;
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
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.CompareTag("Monster"))
                {
                    Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                    if (!hitMonster.IsDead)
                        hitMonster.ChangeHP(-damage);
                }
            }
            battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");
            battleManager.battleCanvas.SetMoveEffect(effectId, target.transform.position); // 임시 이펙트
            character.PlayAnim(CharacterAnim.Slash);
            while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
            character.PlayAnim(CharacterAnim.Idle);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            hit = Physics2D.RaycastAll(character.transform.position, Vector3.right, 1f + (2.5f * range));
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].collider.CompareTag("Monster"))
                {
                    Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                    if (!hitMonster.IsDead)
                        hitMonster.ChangeHP(-damage);
                }
            }
            battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 직선 공격!\n몬스터들에게 {damage}의 데미지 공격!");
            battleManager.battleCanvas.SetMoveEffect(effectId, nextTarget.transform.position); // 임시 이펙트
            character.PlayAnim(CharacterAnim.Slash);
            while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
            character.PlayAnim(CharacterAnim.Idle);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(StraightRangeAttack(damageMultiple, range, effectId));
        }
    }

    IEnumerator DoubleFrozenAttack(int damageMultiple)
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

            battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position);
            yield return character.StartCoroutine(AttackBase(target, damage));
            target.SetFrozen();

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(0, nextTarget.transform.position);
            yield return character.StartCoroutine(AttackBase(nextTarget, damage));
            nextTarget.SetFrozen();

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(FrozenAttack(damageMultiple));
        }
    }

    IEnumerator DoubleStunAttack(int damageMultiple, float duration)
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

            battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position);
            yield return character.StartCoroutine(AttackBase(target, damage));
            target.SetStun(duration);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(0, nextTarget.transform.position);
            yield return character.StartCoroutine(AttackBase(nextTarget, damage));
            nextTarget.SetStun(duration);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(StunAttack(damageMultiple, duration));
        }
    }

    IEnumerator DoubleFlameAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount)
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

            battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position);
            yield return character.StartCoroutine(AttackBase(target, damage));
            target.SetBurn(burnDamage, damageInterval, burnCount);

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(0, nextTarget.transform.position);
            yield return character.StartCoroutine(AttackBase(nextTarget, damage));
            nextTarget.SetBurn(burnDamage, damageInterval, burnCount);

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(FlameAttack(damageMultiple, burnDamage, damageInterval, burnCount));
        }
    }

    IEnumerator FrozenStraightRangeAttack(int damageMultiple, int range, int effectId)
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

    IEnumerator StunStraightRangeAttack(int damageMultiple, float duration, int range, int effectId)
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

    IEnumerator FlameStraightRangeAttack(int damageMultiple, int burnDamage, float damageInterval, int burnCount, int range, int effectId)
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
