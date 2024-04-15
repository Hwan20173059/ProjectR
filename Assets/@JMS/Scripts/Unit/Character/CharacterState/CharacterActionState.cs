using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterActionState : CharacterBaseState
{
    WaitForSeconds waitForAttack = new WaitForSeconds(0.2f);

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
            character.StartCoroutine(BaseAttack());
        }
    }

    void ItemSkill(int itemID)
    {
        switch (itemID)
        {
            case 0: // 정의의 주먹
                character.StartCoroutine(BaseAttack()); break;
            case 1: // 체스말(폰)
                character.StartCoroutine(DoubleAttack()); break;
            case 2: // 나뭇가지
                character.StartCoroutine(Heal()); break;
            case 3: // 낡은 고서
                character.StartCoroutine(HorizontalAttack()); break;
            case 4: // 푸른 장미
                character.StartCoroutine(AllAttack()); break;
            case 5:
                character.StartCoroutine(SpeedUpBuff()); break;
            case 6:
                character.StartCoroutine(RepeatAttack(10)); break;
            case 7:
                character.StartCoroutine(FrozenAttack()); break;
            default:
                character.StartCoroutine(BaseAttack()); break;
        }
    }

    IEnumerator Attack(Monster target, int damage)
    {
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 공격!\n{target.monsterName}에게 {damage}의 피해!");
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);
    }

    IEnumerator BaseAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position); // 임시 이펙트
        yield return character.StartCoroutine(Attack(target, damage));

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttack()
    {
        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            if (!battleManager.monsters[i].IsDead)
            {
                battleManager.monsters[i].ChangeHP(-damage);
                battleManager.battleCanvas.SetRepeatEffect(0, battleManager.monsters[i].transform.position); // 임시 이펙트
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack()
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

            int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position);
            yield return character.StartCoroutine(Attack(target, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            battleManager.battleCanvas.SetRepeatEffect(0, nextTarget.transform.position);
            yield return character.StartCoroutine(Attack(nextTarget, damage));

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            character.ChangeAnimState(CharacterAnimState.Ready);

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(BaseAttack());
        }
    }

    IEnumerator Heal()
    {
        character.ChangeHP(battleManager.rouletteEquip[0].data.tripleValue);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}이 {battleManager.rouletteEquip[0].data.tripleValue}의 체력을 회복!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator HorizontalAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector2.right, 4f);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 가로 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        battleManager.battleCanvas.SetMoveEffect(0, target.transform.position); // 임시 이펙트
        character.PlayAnim(CharacterAnim.Slash);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Slash")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }
        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator SpeedUpBuff()
    {
        character.characterBuffHandler.speedBuff = 300;
        character.characterBuffHandler.speedDuration = 4;

        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 속도가 빨라졌다!");

        character.PlayAnim(CharacterAnim.Jump);
        while (1 > GetNormalizedTime(character.animatorController.animator, "Jump")) { yield return null; }
        character.PlayAnim(CharacterAnim.Idle);

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator RepeatAttack(int count)
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        for(int i = 0; i < count; i++)
        {
            battleManager.battleCanvas.SetRepeatEffect(2, target.transform.position); // 임시 이펙트
            character.StartCoroutine(Attack(target, damage));
            yield return waitForAttack;
            if (target.IsDead) break;
        }

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator FrozenAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = target.transform.position + Vector3.left;

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        battleManager.battleCanvas.SetRepeatEffect(1, target.transform.position); // 임시 이펙트
        yield return character.StartCoroutine(Attack(target, damage));
        target.IsFrozen = true;

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.ChangeAnimState(CharacterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(character.transform.position, target, character.moveAnimSpeed * Time.deltaTime));
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }
}
