using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBaseState : IState
{
    protected BattleStateMachine stateMachine;
    protected BattleManager battleManager { get { return stateMachine.battleManager; } }
    protected BattleCanvas battleCanvas { get { return battleManager.battleCanvas; } }

    public BattleBaseState(BattleStateMachine battleStateMachine)
    {
        stateMachine = battleStateMachine;
    }
    public virtual void Enter()
    {
        
    }

    public virtual void Exit()
    {
        battleManager.SaveCharacterData();
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
