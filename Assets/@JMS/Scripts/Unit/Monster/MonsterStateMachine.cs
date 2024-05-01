using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster monster { get; }
    public MonsterWaitState waitState { get; }
    public MonsterReadyState readyState { get; }
    public MonsterActionSelectingState actionSelectingState { get; }
    public MonsterActionState actionState { get; }
    public MonsterFrozenState frozenState { get; }
    public MonsterStunState stunState { get; }
    public MonsterDeadState deadState { get; }
    public MonsterStateMachine(Monster monster)
    {
        this.monster = monster;

        waitState = new MonsterWaitState(this);
        readyState = new MonsterReadyState(this);
        actionSelectingState = new MonsterActionSelectingState(this);
        actionState = new MonsterActionState(this);
        frozenState = new MonsterFrozenState(this);
        stunState = new MonsterStunState(this);
        deadState = new MonsterDeadState(this);
    }
}
