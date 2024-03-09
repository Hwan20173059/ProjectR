using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateMachine : StateMachine
{
    public BattleManager BattleManager;
    public BattleWaitState WaitState { get; }
    public BattleTakeActionState TakeActionState { get; }
    public BattlePerformActionState PerformActionState {  get; }

    public BattleStateMachine(BattleManager battleManager)
    {
        BattleManager = battleManager;
        
        WaitState = new BattleWaitState(this);
        TakeActionState = new BattleTakeActionState(this);
        PerformActionState = new BattlePerformActionState(this);
    }
}
