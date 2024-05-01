using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : StateMachine
{
    public Character character { get; }
    public CharacterWaitState waitState { get; }
    public CharacterReadyState readyState { get; }
    public CharacterActionSelectingState actionSelectingState { get; }
    public CharacterActionState actionState { get; }
    public CharacterDeadState deadState { get; }
    public CharacterStateMachine(Character character)
    {
        this.character = character;

        waitState = new CharacterWaitState(this);
        readyState = new CharacterReadyState(this);
        actionSelectingState = new CharacterActionSelectingState(this);
        actionState = new CharacterActionState(this);
        deadState = new CharacterDeadState(this);
    }
}
