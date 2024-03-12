using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public BattleManager BattleManager;
    public BattleWaitState WaitState { get; }
    public BattleTakeActionState TakeActionState { get; }
    public BattlePerformActionState PerformActionState {  get; }
    public BattleVictoryState VictoryState { get; }
    public BattleDefeatState DefeatState { get; }
    public BattleEndState EndState { get; }

    public BattleStateMachine(BattleManager battleManager)
    {
        BattleManager = battleManager;
        
        WaitState = new BattleWaitState(this);
        TakeActionState = new BattleTakeActionState(this);
        PerformActionState = new BattlePerformActionState(this);
        EndState = new BattleEndState(this);
        VictoryState = new BattleVictoryState(this);
        DefeatState = new BattleDefeatState(this);
    }
}
