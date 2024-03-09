using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBaseState : IState
{
    protected BattleStateMachine stateMachine;
    public BattleBaseState(BattleStateMachine battleStateMachine)
    {
        stateMachine = battleStateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void HandleInput()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Update()
    {

    }
}
