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

        stateMachine.Monster.battleManager.Character.ChangeExp(stateMachine.Monster.exp);

        stateMachine.Monster.Animator.SetBool("Dead", true);
    }

    public override void Exit()
    {
        base.Exit();

        stateMachine.Monster.Animator.SetBool("Dead", false);
    }
}
