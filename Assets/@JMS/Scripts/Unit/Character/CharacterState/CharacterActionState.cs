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

        switch (stateMachine.Character.selectAction)
        {
            case CharacterAction.Attack:
                CoroutineHelper.StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator Attack()
    {
        selectMonsterPosition = new Vector3(stateMachine.Character.battleManager.selectMonster.startPosition.x -1f, stateMachine.Character.battleManager.selectMonster.startPosition.y, 0);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        stateMachine.Character.Animator.SetBool("Idle", false);
        stateMachine.Character.Animator.SetTrigger("Attack");
        stateMachine.Character.battleManager.selectMonster.ChangeHP(-stateMachine.Character.atk);
        while (!IsAnimationEnd(GetNormalizedTime(stateMachine.Character.Animator, "Attack"))) { yield return null; }
        stateMachine.Character.Animator.SetBool("Idle", true);

        while (MoveTowardsCharacter(stateMachine.Character.startPosition)) { yield return null; }

        stateMachine.Character.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
        stateMachine.Character.battleManager.stateMachine.ChangeState(stateMachine.Character.battleManager.stateMachine.WaitState);
    }

    private bool MoveTowardsCharacter(Vector3 target)
    {
        return target != (stateMachine.Character.transform.position =
            Vector3.MoveTowards(stateMachine.Character.transform.position, target, stateMachine.Character.moveAnimSpeed * Time.deltaTime));
    }
    private bool IsAnimationEnd(float animNormalizedTime)
    {
        return animNormalizedTime >= 1f;
    }
}
