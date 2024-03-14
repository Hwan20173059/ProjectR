using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDeadState : MonsterBaseState
{
    public MonsterDeadState(MonsterStateMachine monsterStateMachine) : base(monsterStateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();

        monster.hpBar.gameObject.SetActive(false);

        character.ChangeExp(monster.exp);

        monster.animator.SetBool("Dead", true);
    }

    public override void Exit()
    {
        base.Exit();

        monster.hpBar.gameObject.SetActive(true);

        monster.animator.SetBool("Dead", false);
    }
}
