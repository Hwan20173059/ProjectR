using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBaseState : IState
{
    protected BattleStateMachine stateMachine;
    protected BattleManager battleManager;
    protected Character character;
    protected IState characterPrevState;
    protected List<Monster> monsters;
    protected IState[] monstersPrevState;
    protected List<int> performList;
    protected BattleCanvas battleCanvas;

    public BattleBaseState(BattleStateMachine battleStateMachine)
    {
        stateMachine = battleStateMachine;
        battleManager = battleStateMachine.battleManager;
        character = battleManager.Character;
        characterPrevState = battleManager.characterPrevState;
        monsters = battleManager.Monsters;
        monstersPrevState = battleManager.monstersPrevState;
        performList = battleManager.PerformList;
        battleCanvas = battleManager.BattleCanvas;
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
