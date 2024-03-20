using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActionState : CharacterBaseState
{
    Vector3 selectMonsterPosition;

    public CharacterActionState(CharacterStateMachine characterStateMachine) : base(characterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (battleManager.rouletteResult == RouletteResult.Triple)
            character.selectAction = CharacterAction.AllAttack;

        switch (character.selectAction)
        {
            case CharacterAction.BaseAttack:
                StateUpdate("공격중");
                character.StartCoroutine(BaseAttack());
                break;
            case CharacterAction.AllAttack:
                StateUpdate("공격중");
                character.StartCoroutine(AllAttack());
                break;
        }
    }

    IEnumerator BaseAttack()
    {
        selectMonster = battleManager.selectMonster;
        selectMonsterPosition = new Vector3(selectMonster.startPosition.x -1f, selectMonster.startPosition.y, 0);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");

        int damage = character.atk;
        battleManager.ChangeValue(battleManager.rouletteResult, ref damage);
        int prevHp = selectMonster.curHP;
        selectMonster.ChangeHP(-damage);
        battleManager.battleCanvas.BattleStateUpdate($"{character.characterName}의 공격!\n{selectMonster.monsterName}에게 {prevHp - selectMonster.curHP}의 피해!");

        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    IEnumerator AllAttack()
    {
        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");

        int damage = character.atk;
        battleManager.ChangeValue(battleManager.rouletteResult, ref damage);

        for (int i = 0; i < battleManager.monsters.Count; i++)
        {
            battleManager.monsters[i].ChangeHP(-damage);
        }
        battleManager.battleCanvas.BattleStateUpdate($"{character.characterName}의 전체 공격!\n몬스터들에게 {damage}의 데미지 공격!");

        while (!IsAnimationEnd(GetNormalizedTime(character.animator, "Attack"))) { yield return null; }
        character.animator.SetBool("Idle", true);

        character.selectAction = CharacterAction.BaseAttack;
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
