using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class MonsterActionState : MonsterBaseState
{
    public MonsterActionState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }
    public override void Enter()
    {
        base.Enter();
        switch (monster.selectAction)
        {
            case MonsterAction.BASEATTACK:
                monster.StartCoroutine(BaseAttack());
                break;
            case MonsterAction.JUMP:
                monster.StartCoroutine(Jump());
                break;
        }
    }

    IEnumerator BaseAttack()
    {
        while (MoveTowardsMonster(monster.attackPosition)) { yield return null; }

        monster.animator.SetBool("Idle", false);
        monster.animator.SetTrigger("BaseAttack");

        int prevHp = character.curHP;
        battleManager.character.ChangeHP(-monster.atk);
        battleManager.battleCanvas.BattleStateUpdate($"{monster.monsterName}의 공격!\n{character.characterName}에게 {prevHp - character.curHP}의 피해!");

        while (!IsAnimationEnd(GetNormalizedTime(monster.animator, "Attack"))) { yield return null; }
        monster.animator.SetBool("Idle", true);

        while (MoveTowardsMonster(monster.startPosition)) { yield return null; }

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
    IEnumerator Jump()
    {
        monster.animator.SetBool("Idle", false);
        monster.animator.SetTrigger("Jump");
        while (!IsAnimationEnd(GetNormalizedTime(monster.animator, "Jump"))) { yield return null; }
        monster.animator.SetBool("Idle", true);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 1f;
    }

    private bool MoveTowardsMonster(Vector3 target)
    {
        return target != (monster.transform.position =
            Vector3.MoveTowards(monster.transform.position, target, monster.moveAnimSpeed * Time.deltaTime));
    }
}
