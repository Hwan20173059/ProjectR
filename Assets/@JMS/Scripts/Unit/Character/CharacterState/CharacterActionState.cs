using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterActionState : CharacterBaseState
{
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
                character.StartCoroutine(BaseAttack());
                break;
            case 1: // 불꽃의 사과
                character.StartCoroutine(DoubleAttack());
                break;
            case 2: // 대지의 빵
                character.StartCoroutine(EatBread());
                break;
            case 3: // 바다의 생선
                character.StartCoroutine(HorizontalAttack());
                break;
            case 4: // 승리의 잔
                character.StartCoroutine(AllAttack());
                break;
            default:
                character.StartCoroutine(BaseAttack());
                break;
        }
    }

    IEnumerator Attack(Monster target, int damage)
    {
        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        int prevHp = target.curHP;
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleState($"{character.characterName}의 공격!\n{target.monsterName}에게 {prevHp - target.curHP}의 피해!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);
    }

    IEnumerator BaseAttack()
    {
        selectMonster = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(selectMonster.startPosition.x -1f, selectMonster.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.atk);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        yield return character.StartCoroutine(Attack(selectMonster, damage));

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttack()
    {
        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.atk);

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            battleManager.monsters[i].ChangeHP(-damage);
        }
        battleManager.battleCanvas.UpdateBattleState($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack()
    {
        if(battleManager.AliveMonsterCount() > 1)
        {
            selectMonster = battleManager.selectMonster;
            Vector3 selectMonsterPosition = new Vector3(selectMonster.startPosition.x - 1f, selectMonster.startPosition.y);
            Monster nextMonster;
            while (true)
            {
                nextMonster = battleManager.monsters[Random.Range(0,battleManager.monsters.Count)];
                if (nextMonster != selectMonster && !nextMonster.IsDead)
                    break;
            }
            Vector3 nextMonsterPosition = new Vector3(nextMonster.startPosition.x - 1f, nextMonster.startPosition.y);
            
            int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.atk);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(selectMonster, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(nextMonster, damage));

            while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

            stateMachine.ChangeState(stateMachine.readyState);
            battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
        }
        else
        {
            character.StartCoroutine(BaseAttack());
        }
    }

    IEnumerator EatBread()
    {
        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Jump");
        int prevHp = character.curHP;
        character.ChangeHP(battleManager.rouletteEquip[0].data.singleValue);
        battleManager.battleCanvas.UpdateBattleState($"{character.characterName}이 {character.curHP - prevHp}의 체력을 회복!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Jump"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator HorizontalAttack()
    {
        selectMonster = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(selectMonster.startPosition.x - 1f, selectMonster.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.atk);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        RaycastHit2D[] hit;
        hit = Physics2D.RaycastAll(character.transform.position, Vector2.right, 4f);
        for(int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Monster"))
            {
                Monster hitMonster = hit[i].collider.GetComponent<Monster>();
                if (!hitMonster.IsDead)
                    hitMonster.ChangeHP(-damage);
            }
        }
        battleManager.battleCanvas.UpdateBattleState($"{character.characterName}의 가로 공격!\n몬스터들에게 {damage}의 데미지 공격!");
        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (character.transform.position =
            Vector3.MoveTowards(character.transform.position, target, character.moveAnimSpeed * Time.deltaTime));
    }
    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 1f;
    }
}
