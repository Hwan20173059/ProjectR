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
        while (MoveTowardsMonster(new Vector3(-5.5f, 1.5f, 0))) { yield return null; }

        monster.Animator.SetBool("Idle", false);
        monster.Animator.SetTrigger("BaseAttack");
        battleManager.Character.ChangeHP(-monster.atk);
        while (!IsAnimationEnd(GetNormalizedTime(monster.Animator, "Attack"))) { yield return null; }
        monster.Animator.SetBool("Idle", true);

        while (MoveTowardsMonster(monster.startPosition)) { yield return null; }

        monster.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.WaitState);
    }
    IEnumerator Jump()
    {
        monster.Animator.SetBool("Idle", false);
        monster.Animator.SetTrigger("Jump");
        while (!IsAnimationEnd(GetNormalizedTime(monster.Animator, "Jump"))) { yield return null; }
        monster.Animator.SetBool("Idle", true);

        monster.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.WaitState);
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
