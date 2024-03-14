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

        character.animator.SetBool("Idle", false);
        character.animator.SetTrigger("Attack");
        selectMonster.ChangeHP(-character.atk);
        if(rouletteResult.Count > 0)
        {
            selectMonster.ChangeHP(-ItemValue(0));
            selectMonster.ChangeHP(-ItemValue(1));
            selectMonster.ChangeHP(-ItemValue(2));
        }
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

    int ItemValue(int idx)
    {
        return rouletteResult[idx].attack;
    }
}
