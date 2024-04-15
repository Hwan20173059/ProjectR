using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public enum MonsterAction
{
    BASEATTACK,
    JUMP
}

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

    IEnumerator Attack(Character target, int damage)
    {
        target.ChangeHP(-damage);
        battleManager.battleCanvas.UpdateBattleText($"{monster.monsterName}의 공격!\n{target.characterName}에게 {damage}의 피해!");
        monster.PlayAnim(MonsterAnim.Attack);
        while (1 > GetNormalizedTime(monster.monsterAnimController.animator, "Attack")) { yield return null; }
        monster.PlayAnim(MonsterAnim.Idle);
    }
    IEnumerator BaseAttack()
    {
        Character target = battleManager.character;
        int damage = monster.atk;

        monster.ChangeAnimState(MonsterAnimState.Running);
        while (MoveTowardsMonster(monster.attackPosition)) { yield return null; }

        battleManager.battleCanvas.SetRepeatEffect(0, target.transform.position); // 임시 이펙트
        yield return character.StartCoroutine(Attack(target, damage));

        while (MoveTowardsMonster(monster.startPosition)) { yield return null; }
        monster.ChangeAnimState(MonsterAnimState.Ready);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }
    IEnumerator Jump()
    {
        monster.PlayAnim(MonsterAnim.Jump);
        while (1 > GetNormalizedTime(monster.monsterAnimController.animator, "Jump")) { yield return null; }
        monster.PlayAnim(MonsterAnim.Idle);

        stateMachine.ChangeState(stateMachine.readyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.waitState);
    }

    private bool MoveTowardsMonster(Vector3 target)
    {
        return target != (monster.transform.position =
            Vector3.MoveTowards(monster.transform.position, target, monster.moveAnimSpeed * Time.deltaTime));
    }
}
