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
    public MonsterDeadState deadState { get; }
    public MonsterStateMachine(Monster monster)
    {
        this.monster = monster;

        waitState = new MonsterWaitState(this);
        readyState = new MonsterReadyState(this);
        selectActionState = new MonsterSelectActionState(this);
        actionState = new MonsterActionState(this);
        deadState = new MonsterDeadState(this);
    }
}
