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
        switch (stateMachine.Monster.selectAction)
        {
            case MonsterAction.BASEATTACK:
                CoroutineHelper.StartCoroutine(BaseAttack());
                break;
            case MonsterAction.JUMP:
                CoroutineHelper.StartCoroutine(Jump());
                break;
        }
    }

    IEnumerator BaseAttack()
    {
        while (MoveTowardsMonster(new Vector3(-5.5f, 1.5f, 0))) { yield return null; }

        stateMachine.Monster.Animator.SetBool("Idle", false);
        stateMachine.Monster.Animator.SetTrigger("BaseAttack");
        stateMachine.Monster.battleManager.Character.TakeDamage(stateMachine.Monster.atk);
        while (!IsAnimationEnd(GetNormalizedTime(stateMachine.Monster.Animator, "Attack"))) { yield return null; }
        stateMachine.Monster.Animator.SetBool("Idle", true);

        while (MoveTowardsMonster(stateMachine.Monster.startPosition)) { yield return null; }

        stateMachine.Monster.battleManager.stateMachine.ChangeState(stateMachine.Monster.battleManager.stateMachine.WaitState);
        stateMachine.Monster.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
    }
    IEnumerator Jump()
    {
        stateMachine.Monster.Animator.SetBool("Idle", false);
        stateMachine.Monster.Animator.SetTrigger("Jump");
        while (!IsAnimationEnd(GetNormalizedTime(stateMachine.Monster.Animator, "Jump"))) { yield return null; }
        stateMachine.Monster.Animator.SetBool("Idle", true);

        stateMachine.Monster.battleManager.stateMachine.ChangeState(stateMachine.Monster.battleManager.stateMachine.WaitState);
        stateMachine.Monster.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
    }

    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 1f;
    }

    private bool MoveTowardsMonster(Vector3 target)
    {
        return target != (stateMachine.Monster.transform.position =
            Vector3.MoveTowards(stateMachine.Monster.transform.position, target, stateMachine.Monster.moveAnimSpeed * Time.deltaTime));
    }
}
