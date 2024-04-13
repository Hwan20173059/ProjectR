using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
            case 1: // 체스말(폰)
                character.StartCoroutine(DoubleAttack());
                break;
            case 2: // 나뭇가지
                character.StartCoroutine(JumpHeal());
                break;
            case 3: // 낡은 고서
                character.StartCoroutine(HorizontalAttack());
                break;
            case 4: // 푸른 장미
                character.StartCoroutine(AllAttack());
                break;
            default:
                character.StartCoroutine(BaseAttack());
                break;
        }
    }

    IEnumerator Attack(Monster target, int damage)
    {
        battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position); // 임시 이펙트
        int prevHp = target.curHP;
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 공격!\n{target.monsterName}에게 {prevHp - target.curHP}의 피해!");
        character.PlayAnim(CharacterAnim.Slash);
        while (!IsAnimationEnd(GetNormalizedTime(character.animatorController.animator, "Slash"))) { yield return null; }
    }

    IEnumerator BaseAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x -1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

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
            battleManager.monsters[i].ChangeHP(-damage);
        }
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        character.PlayAnim(CharacterAnim.Slash);
        while (!IsAnimationEnd(GetNormalizedTime(character.animatorController.animator, "Slash"))) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator DoubleAttack()
    {
        if(battleManager.AliveMonsterCount() > 1)
        {
            Monster target = battleManager.selectMonster;
            Monster nextTarget;
            do
            {
                nextTarget = battleManager.monsters[Random.Range(0, battleManager.monsters.Count)];
            }
            while (nextTarget == target || nextTarget.IsDead);

            Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);
            Vector3 nextMonsterPosition = new Vector3(nextTarget.startPosition.x - 1f, nextTarget.startPosition.y);
            
            int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

            character.ChangeAnimState(CharacterAnimState.Running);

            while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

            yield return character.StartCoroutine(Attack(target, damage));

            while (MoveTowardsCharacter(nextMonsterPosition)) { yield return null; }

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

    IEnumerator JumpHeal()
    {
        int prevHp = character.curHP;
        character.ChangeHP(battleManager.rouletteEquip[0].data.singleValue);
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}이 {character.curHP - prevHp}의 체력을 회복!");

        character.PlayAnim(CharacterAnim.Jump);
        while (!IsAnimationEnd(GetNormalizedTime(character.animatorController.animator, "Jump"))) { yield return null; }

        character.StartCoroutine(BaseAttack());
    }

    IEnumerator HorizontalAttack()
    {
        Monster target = battleManager.selectMonster;
        Vector3 selectMonsterPosition = new Vector3(target.startPosition.x - 1f, target.startPosition.y);

        int damage = battleManager.GetChangeValue(battleManager.rouletteResult, character.changedAtk);

        character.ChangeAnimState(CharacterAnimState.Running);
        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

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
        battleManager.battleCanvas.UpdateBattleText($"{character.characterName}의 가로 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        character.PlayAnim(CharacterAnim.Slash);
        while (!IsAnimationEnd(GetNormalizedTime(character.animatorController.animator, "Slash"))) { yield return null; }

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
    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 0.99f;
    }

    public override void Exit()
    {
        base.Exit();
        character.ReduceBuffDuration();
    }
}
