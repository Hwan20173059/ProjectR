using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public BattleManager battleManager;
    public BattleStartState startState { get; }
    public BattleWaitState waitState { get; }
    public BattlePlayerSelectingActionState playerSelectingActionState { get; }
    public BattleTakeActionState takeActionState { get; }
    public BattlePerformActionState performActionState {  get; }
    public BattleVictoryState victoryState { get; }
    public BattleDefeatState defeatState { get; }
    public BattleEndState endState { get; }

    public BattleStateMachine(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        startState = new BattleStartState(this);
        waitState = new BattleWaitState(this);
        playerSelectingActionState = new BattlePlayerSelectingActionState(this);
        takeActionState = new BattleTakeActionState(this);
        performActionState = new BattlePerformActionState(this);
        endState = new BattleEndState(this);
        victoryState = new BattleVictoryState(this);
        defeatState = new BattleDefeatState(this);
    }
}
