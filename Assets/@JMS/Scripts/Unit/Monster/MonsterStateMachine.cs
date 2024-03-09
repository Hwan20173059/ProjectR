using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster Monster { get; }
    public MonsterWaitState WaitState { get; }
    public MonsterReadyState ReadyState { get; }
    public MonsterSelectActionState SelectActionState { get; }
    public MonsterActionState ActionState { get; }
    public MonsterDeadState DeadState { get; }
    public MonsterStateMachine(Monster monster)
    {
        Monster = monster;

        WaitState = new MonsterWaitState(this);
        ReadyState = new MonsterReadyState(this);
        SelectActionState = new MonsterSelectActionState(this);
        ActionState = new MonsterActionState(this);
        DeadState = new MonsterDeadState(this);
    }
}
