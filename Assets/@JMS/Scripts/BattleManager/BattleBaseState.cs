using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBaseState : IState
{
    protected BattleStateMachine stateMachine;
    protected BattleManager battleManager { get { return stateMachine.battleManager; } }
    protected Character character { get { return battleManager.character; } }
    protected IState characterPrevState { get { return battleManager.characterPrevState; } set { battleManager.characterPrevState = value; } }
    protected List<Monster> monsters { get { return battleManager.monsters; } }
    protected IState monsterPrevState { get { return battleManager.monstersPrevState[monsterPrevStateIndex]; } set { battleManager.monstersPrevState[monsterPrevStateIndex] = value; } }
    protected int monsterPrevStateIndex;
    protected List<int> performList { get { return battleManager.performList; } }
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
