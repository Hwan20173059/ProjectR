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

        switch (character.selectAction)
        {
            case CharacterAction.Attack:
                character.StartCoroutine(Attack());
                break;
        }
    }

    IEnumerator Attack()
    {
        selectMonsterPosition = new Vector3(selectMonster.startPosition.x -1f, selectMonster.startPosition.y, 0);

        while (MoveTowardsCharacter(selectMonsterPosition)) { yield return null; }

        character.Animator.SetBool("Idle", false);
        character.Animator.SetTrigger("Attack");
        selectMonster.ChangeHP(-character.atk);
        while (!IsAnimationEnd(GetNormalizedTime(character.Animator, "Attack"))) { yield return null; }
        character.Animator.SetBool("Idle", true);

        while (MoveTowardsCharacter(character.startPosition)) { yield return null; }

        character.curCoolTime = 0f;
        stateMachine.ChangeState(stateMachine.ReadyState);
        battleManager.stateMachine.ChangeState(battleManager.stateMachine.WaitState);
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
