using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public BattleManager battleManager;
    public BattleStartState startState { get; }
    public BattleWaitState waitState { get; }
    public BattleActionSelectingState actionSelectingState { get; }
    public BattleTakeActionState takeActionState { get; }
    public BattlePerformActionState performActionState {  get; }
    public BattleVictoryState victoryState { get; }
    public BattleDefeatState defeatState { get; }

    public BattleStateMachine(BattleManager battleManager)
    {
        this.battleManager = battleManager;
        startState = new BattleStartState(this);
        waitState = new BattleWaitState(this);
        actionSelectingState = new BattleActionSelectingState(this);
        takeActionState = new BattleTakeActionState(this);
        performActionState = new BattlePerformActionState(this);
        victoryState = new BattleVictoryState(this);
        defeatState = new BattleDefeatState(this);
    }
}
