using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateMachine : StateMachine
{
    public Monster monster { get; }
    public MonsterWaitState waitState { get; }
    public MonsterReadyState readyState { get; }
    public MonsterSelectActionState selectActionState { get; }
    public MonsterActionState actionState { get; }
    public MonsterFrozenState frozenState { get; }
    public MonsterStunState stunState { get; }
    public MonsterDeadState deadState { get; }
    public MonsterStateMachine(Monster monster)
    {
        this.monster = monster;

        waitState = new MonsterWaitState(this);
        readyState = new MonsterReadyState(this);
        selectActionState = new MonsterSelectActionState(this);
        actionState = new MonsterActionState(this);
        frozenState = new MonsterFrozenState(this);
        stunState = new MonsterStunState(this);
        deadState = new MonsterDeadState(this);
    }
}
