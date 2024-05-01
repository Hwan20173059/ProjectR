using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBaseState : IState
{
    protected MonsterStateMachine stateMachine;
    protected Monster monster { get { return stateMachine.monster; } }

    public MonsterBaseState(MonsterStateMachine monsterStateMachine)
    {
        stateMachine = monsterStateMachine;
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
